using AwesomeAssertions;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a SpacesTests.
/// </summary>
[Collection("Dependency Injection")]
public class SpacesTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes GetSpaces_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetSpaces_Succeeds()
	{
		var firstWiki = await GetFirstWikiAsync();

		var result = await XWikiClient.Spaces.GetSpacesAsync(firstWiki.Id, CancellationToken);
		result.Should().NotBeNull();
		result.Spaces.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetSpace_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetSpace_Succeeds()
	{
		var (firstWiki, firstSpace) = await GetFirstWikiAndSpaceAsync();

		var space = await XWikiClient.Spaces.GetSpaceAsync(firstWiki.Id, firstSpace.Id, CancellationToken);
		space.Should().NotBeNull();
	}
}

