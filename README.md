[![](https://img.shields.io/nuget/v/soenneker.ups.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.ups.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.ups.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.ups.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.ups.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.ups.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.ups.openapiclientutil/codeql.yml?style=for-the-badge&label=CodeQL)](https://github.com/soenneker/soenneker.ups.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Ups.OpenApiClientUtil

Provides a cached `UpsOpenApiClient` backed by an HTTP client authenticated with a UPS OAuth access token.

## Installation

```bash
dotnet add package Soenneker.Ups.OpenApiClientUtil
```

## Configuration

```json
{
  "Ups": {
    "AccessToken": "your-oauth-access-token"
  }
}
```

UPS client credentials must be exchanged for the access token separately. This package does not obtain or refresh tokens. `Ups:ApiKey` remains supported as a legacy name for the same token value.

The default production URL is `https://onlinetools.ups.com/api`. Set `Ups:ClientBaseUrl` for a UPS test environment.

## Registration

```csharp
using Soenneker.Ups.OpenApiClientUtil.Registrars;

services.AddUpsOpenApiClientUtilAsScoped();
```

Use `AddUpsOpenApiClientUtilAsSingleton()` to share the generated-client wrapper too. Both registrations use the singleton UPS HTTP provider. Disposing a scoped wrapper does not remove or dispose that shared transport, and the access token is captured when the transport is first created.

## Usage

```csharp
using Soenneker.Ups.OpenApiClient;
using Soenneker.Ups.OpenApiClient.Models;
using Soenneker.Ups.OpenApiClientUtil.Abstract;

public sealed class TrackingReader
{
    private readonly IUpsOpenApiClientUtil _clients;

    public TrackingReader(IUpsOpenApiClientUtil clients)
    {
        _clients = clients;
    }

    public async ValueTask<TrackingTrackApiResponse?> Get(
        string trackingNumber,
        CancellationToken cancellationToken)
    {
        UpsOpenApiClient client = await _clients.Get(cancellationToken);
        string number = Uri.EscapeDataString(trackingNumber);
        string url = $"https://onlinetools.ups.com/api/track/v1/details/{number}";

        return await client.Tracking.Track.V1.Details[trackingNumber]
            .WithUrl(url)
            .GetAsync(
                request => request.QueryParameters.Locale = "en_US",
                cancellationToken);
    }
}
```

`Get()` initializes the generated wrapper once per provider instance. UPS and transport failures propagate through Kiota exceptions.
