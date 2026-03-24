using Soenneker.Ups.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Ups.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IUpsOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<UpsOpenApiClient> Get(CancellationToken cancellationToken = default);
}
