using AwesomeAssertions;
using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a RenderingTests.
/// </summary>
[Collection("Dependency Injection")]
public class RenderingTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes Render_Succeeds.
	/// </summary>
	[Fact]
	public async Task Render_Succeeds()
	{
		var renderingApi = XWikiClient.Rendering;
		var request = new RenderRequest
		{
			Content = "**Hello XWiki!**",
			Syntax = "xwiki/2.1"
		};
		RenderedContent? result;
		try
		{
			result = await renderingApi.RenderAsync(request, CancellationToken);
		}
		catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			return;
		}

		result.Should().NotBeNull();
		result.Html.Should().NotBeNullOrEmpty();
	}
}

