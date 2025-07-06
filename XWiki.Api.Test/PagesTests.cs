using AwesomeAssertions;
using System.Linq;
using System.Threading.Tasks;

namespace XWiki.Api.Test;

[Collection("Dependency Injection")]
public class PagesTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetPages_Succeeds()
	{
		var wikisApi = XWikiClient.GetWikisApi();
		var spacesApi = XWikiClient.GetSpacesApi();
		var pagesApi = XWikiClient.GetPagesApi();
		var wikis = await wikisApi.GetWikisAsync(CancellationToken);
		var firstWiki = wikis.Wikis.FirstOrDefault();
		firstWiki.Should().NotBeNull();
		var spaces = await spacesApi.GetSpacesAsync(firstWiki.Id, CancellationToken);
		var firstSpace = spaces.Spaces.FirstOrDefault();
		firstSpace.Should().NotBeNull();

		var result = await pagesApi.GetPagesAsync(firstWiki.Id, firstSpace.Id, CancellationToken);
		result.Should().NotBeNull();
		result.PageSummaries.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}

	[Fact]
	public async Task GetPage_Succeeds()
	{
		var wikisApi = XWikiClient.GetWikisApi();
		var spacesApi = XWikiClient.GetSpacesApi();
		var pagesApi = XWikiClient.GetPagesApi();
		var wikis = await wikisApi.GetWikisAsync(CancellationToken);
		var firstWiki = wikis.Wikis.FirstOrDefault();
		firstWiki.Should().NotBeNull();
		var spaces = await spacesApi.GetSpacesAsync(firstWiki.Id, CancellationToken);
		var firstSpace = spaces.Spaces.FirstOrDefault();
		firstSpace.Should().NotBeNull();
		var pages = await pagesApi.GetPagesAsync(firstWiki.Id, firstSpace.Id, CancellationToken);
		var firstPage = pages.PageSummaries.FirstOrDefault();
		firstPage.Should().NotBeNull();

		var page = await pagesApi.GetPageAsync(firstWiki.Id, firstSpace.Id, firstPage.Id, CancellationToken);
		page.Should().NotBeNull();
		page.Id.Should().Be(firstPage.Id);
	}
}
