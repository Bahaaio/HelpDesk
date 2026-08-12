using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Dtos.Requests;

public record CreateCommentRequest([Required] [MaxLength(2000)] string Content);