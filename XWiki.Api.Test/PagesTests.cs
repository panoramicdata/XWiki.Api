using AwesomeAssertions;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a PagesTests.
/// </summary>
[Collection("Dependency Injection")]
public class PagesTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes GetPages_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetPages_Succeeds()
	{
		var (firstWiki, firstSpace) = await GetFirstWikiAndSpaceAsync();

		var result = await XWikiClient.Pages.GetPagesAsync(firstWiki.Id, firstSpace.Id, CancellationToken);
		result.Should().NotBeNull();
		result.PageSummaries.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetPage_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetPage_Succeeds()
	{
		// Not all spaces have pages, so this finds one that does.
		var context = await TryGetFirstPageContextAsync();
		if (context is null)
		{
			// No space has any pages, so there is nothing to fetch.
			return;
		}

		var (wiki, space, firstPage) = context.Value;

		var page = await XWikiClient.Pages.GetPageAsync(wiki.Id, space.Id, firstPage.Id, CancellationToken);
		page.Should().NotBeNull();
		page.Id.Should().Be(firstPage.Id);
	}
}

