using Soenneker.Ups.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Ups.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class UpsOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IUpsOpenApiClientUtil _openapiclientutil;

    public UpsOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IUpsOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
