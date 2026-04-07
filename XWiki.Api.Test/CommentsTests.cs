using AwesomeAssertions;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a CommentsTests.
/// </summary>
[Collection("Dependency Injection")]
public class CommentsTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes GetComments_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetComments_Succeeds()
	{
		var commentsApi = XWikiClient.Comments;
		var context = await TryGetFirstPageContextAsync();
		if (context is null)
		{
			return;
		}

		var result = await commentsApi.GetCommentsAsync(context.Value.Wiki.Id, context.Value.Space.Id, context.Value.Page.Id, CancellationToken);
		result.Should().NotBeNull();
		result.Comments.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}

