using Microsoft.AspNetCore.Builder;

namespace TM.SecureApiKit;

public static class ErrorHandlingExtensions
{
    public static IApplicationBuilder UseSecureErrorHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}