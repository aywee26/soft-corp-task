using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoftCorpTask.Exceptions;

namespace SoftCorpTask.ExceptionHandlers;

public class CandidateNotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not CandidateNotFoundException)
            return false;
        
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Candidate not found",
            Detail = exception.Message
        };
        
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}