namespace HelpDesk.Api.Dtos.Responses;

public record CommentDto(
    int Id,
    string Content,
    DateTime CreatedAt,
    string AuthorUsername
);