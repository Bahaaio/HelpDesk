namespace HelpDesk.Dtos.Responses;

public record AttachmentResult(Stream Stream, string ContentType, string OriginalFileName);
