namespace HelpDesk.Modules.Attachments.Dtos;

public record AttachmentDto(Guid AttachmentId, string ContentType, string OriginalFileName);