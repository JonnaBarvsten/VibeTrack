using Microsoft.AspNetCore.Mvc;

namespace VibeTrack.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "Ett oväntat fel har inträffat"
                };

                await context.Response.WriteAsJsonAsync(problemDetails);

            }
        }
    }
}
