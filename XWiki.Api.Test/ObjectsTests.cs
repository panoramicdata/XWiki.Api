using AwesomeAssertions;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a ObjectsTests.
/// </summary>
[Collection("Dependency Injection")]
public class ObjectsTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes GetObjects_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetObjects_Succeeds()
	{
		var objectsApi = XWikiClient.Objects;
		var context = await TryGetFirstPageContextAsync();
		if (context is null)
		{
			return;
		}

		var result = await objectsApi.GetObjectsAsync(context.Value.Wiki.Id, context.Value.Space.Id, context.Value.Page.Id, CancellationToken);
		result.Should().NotBeNull();
		result.Objects.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}

