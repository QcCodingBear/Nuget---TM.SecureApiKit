using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace TM.SecureApiKit;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ErrorHandlingOptions _options;

    public ErrorHandlingMiddleware(RequestDelegate next, IOptions<ErrorHandlingOptions> options)
    {
        _next = next;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Si la réponse a déjà commencé, on ne peut plus écrire du JSON propre
        if (context.Response.HasStarted)
        {
            return;
        }

        var statusCode = MapExceptionToStatusCode(exception);

        var response = new Dictionary<string, object?>
        {
            ["traceId"] = context.TraceIdentifier,
            ["status"] = statusCode,
            ["path"] = context.Request.Path.Value,
            ["method"] = context.Request.Method,
            ["timestamp"] = DateTime.UtcNow.ToString("o")
        };

        if (_options.IncludeExceptionMessage)
            response["message"] = exception.Message;

        if (_options.IncludeExceptionType)
            response["type"] = exception.GetType().Name;

        if (_options.IncludeExceptionDetails)
            response["details"] = exception.StackTrace;

        if (!string.IsNullOrWhiteSpace(_options.SupportEmail))
            response["support"] = _options.SupportEmail;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(json);
    }

    private int MapExceptionToStatusCode(Exception ex)
    {
        return ex switch
        {
            ArgumentException => (int)HttpStatusCode.BadRequest,          // 400
            UnauthorizedAccessException => (int)HttpStatusCode.Forbidden, // 403
            KeyNotFoundException => (int)HttpStatusCode.NotFound,         // 404
            _ => (int)HttpStatusCode.InternalServerError                  // 500
        };
    }
}