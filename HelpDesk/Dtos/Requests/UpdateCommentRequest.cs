using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Dtos.Requests;

public record UpdateCommentRequest([Required] [MaxLength(2000)] string Content);
