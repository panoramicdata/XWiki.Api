using AwesomeAssertions;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a ClassesTests.
/// </summary>
[Collection("Dependency Injection")]
public class ClassesTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes GetClasses_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetClasses_Succeeds()
	{
		var firstWiki = await GetFirstWikiAsync();

		var result = await XWikiClient.Classes.GetClassesAsync(firstWiki.Id, CancellationToken);
		result.Should().NotBeNull();
		result.Classes.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}

