using Serilog;

namespace RoleRollsPocketEdition.Core.MIddlewares;

public class SerilogBadRequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public SerilogBadRequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;
        try
        {
            await _next(context);

            // Rewind the buffer so we can read/copy it
            responseBodyStream.Seek(0, SeekOrigin.Begin);

            if (context.Response.StatusCode == StatusCodes.Status400BadRequest)
            {
                var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                responseBodyStream.Seek(0, SeekOrigin.Begin);

                Log.Warning(
                    "BadRequest detected: Path={Path}, Query={Query}, User={User}, Response={ResponseBody}",
                    context.Request.Path,
                    context.Request.QueryString,
                    context.User.Identity?.Name ?? "Anonymous",
                    responseBody);
            }

            // Ensure the response body is correctly written to the original body stream
            await responseBodyStream.CopyToAsync(originalBodyStream);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }
}
