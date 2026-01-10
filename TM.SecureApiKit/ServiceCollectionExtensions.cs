using Microsoft.Extensions.DependencyInjection;

namespace TM.SecureApiKit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSecureErrorHandling(
        this IServiceCollection services,
        Action<ErrorHandlingOptions>? configure = null)
    {
        if (configure != null)
            services.Configure(configure);
        else
            services.Configure<ErrorHandlingOptions>(_ => { });

        return services;
    }
}