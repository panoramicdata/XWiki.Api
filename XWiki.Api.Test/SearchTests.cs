using AwesomeAssertions;
using System.Threading.Tasks;

namespace XWiki.Api.Test;

[Collection("Dependency Injection")]
public class SearchTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task Search_Succeeds()
	{
		var searchApi = XWikiClient.Search;
		var result = await searchApi.SearchAsync("XWiki", CancellationToken);
		result.Should().NotBeNull();
		result.Results.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}
