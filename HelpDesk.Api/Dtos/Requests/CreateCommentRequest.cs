using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Dtos.Requests;

public record CreateCommentRequest([MaxLength(2000)] string Content);