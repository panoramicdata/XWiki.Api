using AwesomeAssertions;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a HistoryTests.
/// </summary>
[Collection("Dependency Injection")]
public class HistoryTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes GetHistory_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetHistory_Succeeds()
	{
		var historyApi = XWikiClient.History;
		var context = await TryGetFirstPageContextAsync();
		if (context is null)
		{
			return;
		}

		var result = await historyApi.GetHistoryAsync(context.Value.Wiki.Id, context.Value.Space.Id, context.Value.Page.Id, CancellationToken);
		result.Should().NotBeNull();
		result.History.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}

