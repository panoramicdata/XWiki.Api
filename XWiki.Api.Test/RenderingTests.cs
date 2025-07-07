using AwesomeAssertions;
using System.Threading.Tasks;
using XWiki.Api.Models;

namespace XWiki.Api.Test;

[Collection("Dependency Injection")]
public class RenderingTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	[Fact]
	public async Task Render_Succeeds()
	{
		var renderingApi = XWikiClient.Rendering;
		var request = new RenderRequest
		{
			Content = "**Hello XWiki!**",
			Syntax = "xwiki/2.1"
		};
		var result = await renderingApi.RenderAsync(request, CancellationToken);
		result.Should().NotBeNull();
		result.Html.Should().NotBeNullOrEmpty();
	}
}
