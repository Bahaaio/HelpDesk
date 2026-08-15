namespace HelpDesk.Api.Dtos.Responses;

public record AttachmentDto(Guid AttachmentId, string ContentType, string OriginalFileName);