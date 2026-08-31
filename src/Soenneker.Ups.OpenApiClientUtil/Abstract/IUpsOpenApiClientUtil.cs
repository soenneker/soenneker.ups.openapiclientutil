using Soenneker.Ups.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Ups.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a cached UPS OpenAPI client backed by authenticated transport.
/// </summary>
public interface IUpsOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the client, creating it on first use.
    /// </summary>
    /// <param name="cancellationToken">The token used to cancel client creation.</param>
    /// <returns>The cached client.</returns>
    ValueTask<UpsOpenApiClient> Get(CancellationToken cancellationToken = default);
}
