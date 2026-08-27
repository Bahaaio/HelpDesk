using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Comments.Dtos.Requests;

public record UpdateCommentRequest([Required] [MaxLength(2000)] string Content);
