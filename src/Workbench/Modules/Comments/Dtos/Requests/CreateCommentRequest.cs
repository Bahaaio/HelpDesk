using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Comments.Dtos.Requests;

public record CreateCommentRequest([Required] [MaxLength(2000)] string Content);
