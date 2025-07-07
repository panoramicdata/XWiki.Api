using AwesomeAssertions;
using System.Linq;
using System.Threading.Tasks;
using XWiki.Api.Models;

namespace XWiki.Api.Test;

[Collection("Dependency Injection")]
public class PagesTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetPages_Succeeds()
	{
		var wikisApi = XWikiClient.Wikis;
		var spacesApi = XWikiClient.Spaces;
		var pagesApi = XWikiClient.Pages;
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
		var wikisApi = XWikiClient.Wikis;
		var spacesApi = XWikiClient.Spaces;
		var pagesApi = XWikiClient.Pages;
		var wikis = await wikisApi.GetWikisAsync(CancellationToken);
		var firstWiki = wikis.Wikis.FirstOrDefault();
		firstWiki.Should().NotBeNull();
		var spaces = await spacesApi.GetSpacesAsync(firstWiki.Id, CancellationToken);

		// Not all spaces have pages, so we need to ensure we have at least one space with pages.

		// Iterate over all spaces and find one that has pages.

		spaces.Spaces.Should().NotBeNull();
		spaces.Spaces.Should().NotBeEmpty();

		PagesResponse? pages = null;
		Space? testSpace = null;
		foreach (var space in spaces.Spaces)
		{
			pages = await pagesApi.GetPagesAsync(firstWiki.Id, space.Id, CancellationToken);
			if (pages?.PageSummaries.Length > 0)
			{
				testSpace = space;
				break;
			}
		}

		if (testSpace is null)
		{
			// If no space with pages was found, we cannot proceed with the test.
			return;
		}

		pages.Should().NotBeNull();
		var firstPage = pages.PageSummaries.FirstOrDefault();
		firstPage.Should().NotBeNull();
		var page = await pagesApi.GetPageAsync(firstWiki.Id, testSpace.Id, firstPage.Id, CancellationToken);
		page.Should().NotBeNull();
		page.Id.Should().Be(firstPage.Id);
	}
}
