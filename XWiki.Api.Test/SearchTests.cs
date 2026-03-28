using AwesomeAssertions;

namespace XWiki.Api.Test;

[Collection("Dependency Injection")]
public class SearchTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task Search_Succeeds()
	{
		// Get wiki name from config
		var wikiName = TestConfig.WikiName;

		var searchApi = XWikiClient.Search;
		var result = await searchApi.SearchAsync(wikiName, "XWiki", CancellationToken);
		result.Should().NotBeNull();
		result.Results.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}
