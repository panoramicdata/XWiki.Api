using AwesomeAssertions;
using System.Threading.Tasks;

namespace XWiki.Api.Test;

public class ServerInfoTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetVersion_Succeeds()
	{
		var result = await XWikiClient
			.ServerInfo
			.GetAsync(CancellationToken);

		result.Should().NotBeNull();
		result.Links.Should().NotBeNull();
		result.Links.Should().NotBeEmpty();
		result.Version.Should().NotBeNullOrEmpty();
		result.Version.Should().NotBe("0.0.0");
	}
}