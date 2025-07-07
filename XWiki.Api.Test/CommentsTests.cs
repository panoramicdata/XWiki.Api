using AwesomeAssertions;
using System.Linq;
using System.Threading.Tasks;

namespace XWiki.Api.Test;

[Collection("Dependency Injection")]
public class CommentsTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetComments_Succeeds()
	{
		var wikisApi = XWikiClient.Wikis;
		var spacesApi = XWikiClient.Spaces;
		var pagesApi = XWikiClient.Pages;
		var commentsApi = XWikiClient.Comments;
		var wikis = await wikisApi.GetWikisAsync(CancellationToken);
		var firstWiki = wikis.Wikis.FirstOrDefault();
		firstWiki.Should().NotBeNull();
		var spaces = await spacesApi.GetSpacesAsync(firstWiki.Id, CancellationToken);
		var firstSpace = spaces.Spaces.FirstOrDefault();
		firstSpace.Should().NotBeNull();
		var pages = await pagesApi.GetPagesAsync(firstWiki.Id, firstSpace.Id, CancellationToken);
		var firstPage = pages.PageSummaries.FirstOrDefault();
		firstPage.Should().NotBeNull();

		var result = await commentsApi.GetCommentsAsync(firstWiki.Id, firstSpace.Id, firstPage.Id, CancellationToken);
		result.Should().NotBeNull();
		result.Comments.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}
