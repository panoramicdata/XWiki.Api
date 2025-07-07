using AwesomeAssertions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace XWiki.Api.Test;

[Collection("Dependency Injection")]
public class SearchTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task Search_Succeeds()
	{
		// Get wiki name from config
		var testConfig = fixture.GetService<IOptions<TestConfig>>(testOutputHelper)?.Value;
		var wikiName = testConfig?.WikiName ?? "xwiki";

		var searchApi = XWikiClient.Search;
		var result = await searchApi.SearchAsync(wikiName, "XWiki", CancellationToken);
		result.Should().NotBeNull();
		result.Results.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}
