using AwesomeAssertions;
using System.Linq;
using System.Threading.Tasks;

namespace XWiki.Api.Test;

[Collection("Dependency Injection")]
public class ClassesTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task GetClasses_Succeeds()
	{
		var wikisApi = XWikiClient.Wikis;
		var classesApi = XWikiClient.Classes;
		var wikis = await wikisApi.GetWikisAsync(CancellationToken);
		var firstWiki = wikis.Wikis.FirstOrDefault();
		firstWiki.Should().NotBeNull();

		var result = await classesApi.GetClassesAsync(firstWiki.Id, CancellationToken);
		result.Should().NotBeNull();
		result.Classes.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}
