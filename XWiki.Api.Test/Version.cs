using AwesomeAssertions;
using Xunit.Abstractions;

namespace XWiki.Api.Test;

public class Version(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetVersion_Succeeds()
	{
		var result = await XWikiClient
			.ServerInfo
			.GetAsync(default);

		result.Should().NotBeNull();
		result.Links.Should().NotBeNull();
		result.Links.Should().NotBeEmpty();
		result.Version.Should().NotBeNullOrEmpty();
		result.Version.Should().NotBe("0.0.0");
	}
}