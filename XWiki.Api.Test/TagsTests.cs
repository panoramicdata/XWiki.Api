using AwesomeAssertions;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a TagsTests.
/// </summary>
[Collection("Dependency Injection")]
public class TagsTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes GetTags_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetTags_Succeeds()
	{
		var tagsApi = XWikiClient.Tags;
		var context = await TryGetFirstPageContextAsync();
		if (context is null)
		{
			return;
		}

		var result = await tagsApi.GetTagsAsync(context.Value.Wiki.Id, context.Value.Space.Id, context.Value.Page.Id, CancellationToken);
		result.Should().NotBeNull();
		result.Tags.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}

