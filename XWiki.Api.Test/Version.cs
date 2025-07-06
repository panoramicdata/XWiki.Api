using AwesomeAssertions;
using Xunit.Abstractions;

namespace XWiki.Api.Test;

public class Version(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetVersion_Succeeds()
	{
		var result = await XWikiClient.Version.GetAsync(default);
		result.Should().NotBeNull();
	}
}
