using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Ups.HttpClients.Abstract;
using Soenneker.Ups.OpenApiClientUtil.Abstract;
using Soenneker.Ups.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Ups.OpenApiClientUtil;

///<inheritdoc cref="IUpsOpenApiClientUtil"/>
public sealed class UpsOpenApiClientUtil : IUpsOpenApiClientUtil
{
    private readonly AsyncSingleton<UpsOpenApiClient> _client;

    public UpsOpenApiClientUtil(IUpsOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<UpsOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Ups:ApiKey");
            string authHeaderValueTemplate = configuration["Ups:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new UpsOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<UpsOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
