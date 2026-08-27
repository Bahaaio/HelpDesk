using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Workbench.Common.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // catch all
        if (exception is not BaseException ex)
            throw exception;
        // ex = new InternalServerErrorException("An unexpected error occurred.");

        httpContext.Response.StatusCode = ex.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = ex.StatusCode,
            Title = ex.Title,
            Detail = ex.Message,
            Type = $"https://httpstatuses.com/{ex.StatusCode}"
        }, cancellationToken);

        return true;
    }
}
