using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Modules.Comments.Dtos.Requests;

public record UpdateCommentRequest([Required] [MaxLength(2000)] string Content);