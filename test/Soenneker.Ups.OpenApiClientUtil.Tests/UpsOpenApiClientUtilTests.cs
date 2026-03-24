using Soenneker.Ups.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Ups.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class UpsOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IUpsOpenApiClientUtil _openapiclientutil;

    public UpsOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IUpsOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
