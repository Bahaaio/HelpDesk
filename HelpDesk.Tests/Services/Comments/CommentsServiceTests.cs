using HelpDesk.Common.Exceptions;
using HelpDesk.Data;
using HelpDesk.Modules.Auth.Models;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Authorization.Requirements;
using HelpDesk.Modules.Authorization.Services;
using HelpDesk.Modules.Comments.Dtos.Requests;
using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Comments.Services.Implementations;
using HelpDesk.Modules.Issues.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace HelpDesk.Tests.Services.Comments;

public class CommentsServiceTests : IDisposable
{
    private const int CurrentUserId = 123;
    private const int DefaultIssueId = 1; // Added a constant for the default issue

    private readonly Mock<IAuthorizationGuard> _authGuardMock;
    private readonly SqliteConnection _connection;
    private readonly AppDbContext _db;
    private readonly CommentsService _service;

    public CommentsServiceTests()
    {
        // 1. Set up SQLite In-Memory Database
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        _db = new AppDbContext(options);
        _db.Database.EnsureCreated();

        // 2. Setup Mocks
        var userMock = new Mock<ICurrentUser>();
        userMock.Setup(u => u.Id).Returns(CurrentUserId);

        var loggerMock = new Mock<ILogger<CommentsService>>();
        _authGuardMock = new Mock<IAuthorizationGuard>();

        _service =
            new CommentsService(_db, userMock.Object, loggerMock.Object, _authGuardMock.Object);

        // 3. Seed Base Data that is used by almost all tests
        _db.Users.Add(new ApplicationUser
            { Id = CurrentUserId, UserName = "test", Email = "a@b.com" });

        _db.Issues.Add(
            new Issue { Id = DefaultIssueId, Title = "new", AuthorId = CurrentUserId });

        _db.SaveChanges();
    }

    public void Dispose()
    {
        _db.Database.EnsureDeleted();
        _db.Dispose();
        _connection.Close();
        _connection.Dispose();
    }

    [Fact]
    public async Task GetAll_ReturnsOrderedComments_WhenIssueExists()
    {
        // Arrange (Issue is already seeded in constructor)
        _db.Comments.AddRange(
            new Comment
            {
                Id = 1, IssueId = DefaultIssueId, Content = "Oldest",
                CreatedAt = DateTime.UtcNow.AddMinutes(-10), AuthorId = CurrentUserId
            },
            new Comment
            {
                Id = 2, IssueId = DefaultIssueId, Content = "Newest", CreatedAt = DateTime.UtcNow,
                AuthorId = CurrentUserId
            }
        );
        await _db.SaveChangesAsync();

        // Act
        var result = await _service.GetAll(DefaultIssueId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Newest", result.First().Content);
    }

    [Fact]
    public async Task GetAll_ThrowsNotFoundException_WhenIssueDoesNotExist()
    {
        // Arrange
        const int nonExistentIssueId = 999;

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetAll(nonExistentIssueId));
    }

    [Fact]
    public async Task Create_AddsCommentAndReturnsDto_WhenIssueExists()
    {
        // Arrange
        var request = new CreateCommentRequest("Test comment content");

        // Act
        var result = await _service.Create(DefaultIssueId, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Content, result.Content);

        var savedComment =
            await _db.Comments.FirstOrDefaultAsync(c => c.IssueId == DefaultIssueId);
        Assert.NotNull(savedComment);
        Assert.Equal(request.Content, savedComment.Content);
        Assert.Equal(CurrentUserId, savedComment.AuthorId);
    }

    [Fact]
    public async Task Update_ChangesContentAndReturnsDto_WhenAuthorized()
    {
        // Arrange
        const int commentId = 1;
        var comment = new Comment
        {
            Id = commentId, IssueId = DefaultIssueId, AuthorId = CurrentUserId,
            Content = "original"
        };
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        _authGuardMock
            .Setup(g => g.Authorize(comment, It.IsAny<OwnerOrTechnicianRequirement>()))
            .Returns(Task.CompletedTask);

        var request = new UpdateCommentRequest("updated content");

        // Act
        var result = await _service.Update(commentId, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Content, result.Content);

        var savedComment = await _db.Comments.FindAsync(commentId);
        Assert.NotNull(savedComment);
        Assert.Equal(request.Content, savedComment!.Content);
    }

    [Fact]
    public async Task Update_ThrowsException_WhenUnauthorized()
    {
        // Arrange
        const int commentId = 1;
        const int secondUserId = 2;

        _db.Users.Add(new ApplicationUser
            { Id = secondUserId, UserName = "test2", Email = "b@b.com" });

        var comment = new Comment
        {
            Id = commentId, IssueId = DefaultIssueId, AuthorId = secondUserId, Content = "hello"
        };
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        _authGuardMock
            .Setup(g => g.Authorize(comment, It.IsAny<OwnerOrTechnicianRequirement>()))
            .ThrowsAsync(new UnauthorizedAccessException("User is not authorized."));

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _service.Update(commentId, new UpdateCommentRequest("hijacked")));

        var unchangedComment = await _db.Comments.FindAsync(commentId);
        Assert.NotNull(unchangedComment);
        Assert.Equal("hello", unchangedComment!.Content);
    }

    [Fact]
    public async Task Delete_RemovesComment_WhenAuthorized()
    {
        // Arrange
        const int commentId = 1;
        var comment = new Comment
        {
            Id = commentId, IssueId = DefaultIssueId, AuthorId = CurrentUserId,
            Content = "comment"
        };
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        _authGuardMock
            .Setup(g => g.Authorize(comment, It.IsAny<OwnerOrTechnicianRequirement>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.Delete(commentId);

        // Assert
        var deletedComment = await _db.Comments.FindAsync(commentId);
        Assert.Null(deletedComment);

        _authGuardMock.Verify(g =>
                g.Authorize(It.IsAny<Comment>(), It.IsAny<IAuthorizationRequirement>()),
            Times.Once);
    }

    [Fact]
    public async Task Delete_ThrowsException_WhenUnauthorized()
    {
        // Arrange
        const int commentId = 1;
        const int secondUserId = 2;

        // Add the second user and their comment (Issue is already seeded)
        _db.Users.Add(new ApplicationUser
            { Id = secondUserId, UserName = "test2", Email = "b@b.com" });

        var comment = new Comment
        {
            Id = commentId, IssueId = DefaultIssueId, AuthorId = secondUserId, Content = "hello"
        };
        _db.Comments.Add(comment);

        await _db.SaveChangesAsync();

        _authGuardMock
            .Setup(g => g.Authorize(comment, It.IsAny<OwnerOrTechnicianRequirement>()))
            .ThrowsAsync(new UnauthorizedAccessException("User is not authorized."));

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.Delete(commentId));

        var stillExistingComment = await _db.Comments.FindAsync(commentId);
        Assert.NotNull(stillExistingComment);
    }
}