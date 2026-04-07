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
		var wikisApi = XWikiClient.Wikis;
		var spacesApi = XWikiClient.Spaces;
		var wikis = await wikisApi.GetWikisAsync(CancellationToken);
		var firstWiki = wikis.Wikis.FirstOrDefault();
		firstWiki.Should().NotBeNull();

		var result = await spacesApi.GetSpacesAsync(firstWiki.Id, CancellationToken);
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
		var wikisApi = XWikiClient.Wikis;
		var spacesApi = XWikiClient.Spaces;
		var wikis = await wikisApi.GetWikisAsync(CancellationToken);
		var firstWiki = wikis.Wikis.FirstOrDefault();
		firstWiki.Should().NotBeNull();
		var spaces = await spacesApi.GetSpacesAsync(firstWiki.Id, CancellationToken);
		var firstSpace = spaces.Spaces.FirstOrDefault();
		firstSpace.Should().NotBeNull();

		var space = await spacesApi.GetSpaceAsync(firstWiki.Id, firstSpace.Id, CancellationToken);
		space.Should().NotBeNull();
	}
}

