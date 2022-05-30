using System.Net;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters;

internal sealed class ExceptionActionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.SelectMany(value => value.Errors.Select(error => error.ErrorMessage)).ToArray();
            context.Result = new BadRequestObjectResult(new ErrorResponseModel(errors));
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null)
        {
            return;
        }

        switch (context.Exception)
        {
            case DomainException domainException:
                context.Result = new BadRequestObjectResult(new ErrorResponseModel(domainException.Message));
                context.ExceptionHandled = true;
                return;
            default:
                context.Result = new ObjectResult(new { }) {StatusCode = (int) HttpStatusCode.InternalServerError};
                context.ExceptionHandled = true;
                return;
        }
    }

    public int Order => int.MaxValue - 10;

    private sealed record ErrorResponseModel(params string[] Errors);
}