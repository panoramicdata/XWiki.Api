using AwesomeAssertions;
using System.Linq;
using System.Threading.Tasks;

namespace XWiki.Api.Test;

[Collection("Dependency Injection")]
public class WikisTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetWikis_Succeeds()
	{
		var wikisApi = XWikiClient.GetWikisApi();
		var result = await wikisApi.GetWikisAsync(CancellationToken);

		result.Should().NotBeNull();
		result.Wikis.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}

	[Fact]
	public async Task GetWiki_Succeeds()
	{
		var wikisApi = XWikiClient.GetWikisApi();
		var wikis = await wikisApi.GetWikisAsync(CancellationToken);
		var firstWiki = wikis.Wikis.FirstOrDefault();
		firstWiki.Should().NotBeNull();

		var wiki = await wikisApi.GetWikiAsync(firstWiki.Id, CancellationToken);
		wiki.Should().NotBeNull();
		wiki.Id.Should().Be(firstWiki.Id);
	}
}
