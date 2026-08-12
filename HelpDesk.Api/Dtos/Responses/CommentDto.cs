namespace HelpDesk.Api.Dtos.Responses;

public record CommentDto(
    string Content,
    DateTime CreatedAt,
    string AuthorUsername
);