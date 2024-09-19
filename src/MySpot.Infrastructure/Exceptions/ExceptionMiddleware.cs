using Humanizer;
using Microsoft.AspNetCore.Http;
using MySpot.Core.Exceptions;

namespace MySpot.Infrastructure.Exceptions;
internal sealed class ExceptionMiddleware : IMiddleware
{
    async public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.ToString());
            await HandleException(exception, context);
        }
    }



    async private Task HandleException(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            CustomException => (StatusCodes.Status400BadRequest, new Error(exception.GetType().Name.Underscore().Replace("_exception", ""), exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, new Error("error", "There was an error"))
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

    private record Error(string Code, string Reason);
}