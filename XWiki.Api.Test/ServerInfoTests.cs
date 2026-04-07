using AwesomeAssertions;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a ServerInfoTests.
/// </summary>
public class ServerInfoTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes GetVersion_Succeeds.
	/// </summary>
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
