using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Ups.HttpClients.Registrars;
using Soenneker.Ups.OpenApiClientUtil.Abstract;

namespace Soenneker.Ups.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class UpsOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="UpsOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddUpsOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddUpsOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IUpsOpenApiClientUtil, UpsOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="UpsOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddUpsOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddUpsOpenApiHttpClientAsSingleton()
                .TryAddScoped<IUpsOpenApiClientUtil, UpsOpenApiClientUtil>();

        return services;
    }
}
