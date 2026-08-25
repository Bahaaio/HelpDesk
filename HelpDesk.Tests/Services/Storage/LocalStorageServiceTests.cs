using System.Text;
using HelpDesk.Modules.Storage.Services.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HelpDesk.Tests.Services.Storage;

public class LocalStorageServiceTests : IDisposable
{
    private readonly Mock<IWebHostEnvironment> _envMock;
    private readonly string _tempRootPath;
    private readonly string _uploadsPath;

    public LocalStorageServiceTests()
    {
        // 1. Create a unique temporary directory for each test run
        _tempRootPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        _uploadsPath = Path.Combine(_tempRootPath, "Uploads");

        // 2. Mock the environment to return our temporary path
        _envMock = new Mock<IWebHostEnvironment>();

        _envMock
            .Setup(e => e.ContentRootPath)
            .Returns(_tempRootPath);
    }

    public void Dispose()
    {
        // 3. Clean up the physical files after the test completes
        if (Directory.Exists(_tempRootPath)) Directory.Delete(_tempRootPath, true);
    }

    [Fact]
    public void Constructor_CreatesStorageDirectory()
    {
        // Act
        _ = new LocalStorageService(_envMock.Object);

        // Assert
        Assert.True(Directory.Exists(_uploadsPath));
    }

    [Fact]
    public async Task Store_SavesFileToDisk()
    {
        // Arrange
        var service = new LocalStorageService(_envMock.Object);
        const string testKey = "test-document.txt";
        var expectedPath = Path.Combine(_uploadsPath, testKey);

        var fileMock = new Mock<IFormFile>();
        const string content = "Hello, HelpDesk!";
        var ms = new MemoryStream(Encoding.UTF8.GetBytes(content));

        // Simulate IFormFile.CopyToAsync writing data into the provided stream
        fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Callback<Stream, CancellationToken>((stream, token) => { ms.CopyTo(stream); })
            .Returns(Task.CompletedTask);

        // Act
        await service.Store(fileMock.Object, testKey);

        // Assert
        Assert.True(File.Exists(expectedPath));
        var savedContent = await File.ReadAllTextAsync(expectedPath);
        Assert.Equal(content, savedContent);

        fileMock.Verify(f =>
                f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    public async Task Load_ReturnsStream_WhenFileExists()
    {
        // Arrange
        var service = new LocalStorageService(_envMock.Object);
        const string testKey = "existing-file.txt";
        var filePath = Path.Combine(_uploadsPath, testKey);

        // Manually seed a file into the temp directory
        const string content = "Mock file content";
        await File.WriteAllTextAsync(filePath, content);

        // Act
        await using var stream = await service.Load(testKey);

        // Assert
        Assert.NotNull(stream);
        using var reader = new StreamReader(stream);
        var actualContent = await reader.ReadToEndAsync();
        Assert.Equal(content, actualContent);
    }

    [Fact]
    public async Task Load_ReturnsNull_WhenFileDoesNotExist()
    {
        // Arrange
        var service = new LocalStorageService(_envMock.Object);
        const string testKey = "missing-file.txt";

        // Act
        var stream = await service.Load(testKey);

        // Assert
        Assert.Null(stream);
    }

    [Fact]
    public async Task DeleteFile_RemovesFile_WhenFileExists()
    {
        // Arrange
        var service = new LocalStorageService(_envMock.Object);
        const string testKey = "file-to-delete.txt";
        var filePath = Path.Combine(_uploadsPath, testKey);

        await File.WriteAllTextAsync(filePath, "content to delete");

        // Act
        await service.DeleteFile(testKey);

        // Assert
        Assert.False(File.Exists(filePath));
    }

    [Fact]
    public async Task DeleteFile_DoesNotThrow_WhenFileDoesNotExist()
    {
        // Arrange
        var service = new LocalStorageService(_envMock.Object);
        const string testKey = "already-missing-file.txt";
        var filePath = Path.Combine(_uploadsPath, testKey);

        // Act
        var exception = await Record.ExceptionAsync(() => service.DeleteFile(testKey));

        // Assert
        Assert.Null(exception);
        Assert.False(File.Exists(filePath));
    }
}