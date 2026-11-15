using AwesomeAssertions;
using XWiki.Api.Models;

namespace XWiki.Api.Test.Offline;

/// <summary>
/// Offline tests that send every client call to a loopback stub and check the request built and the model parsed.
/// </summary>
public class XWikiEndpointTests
{
	private const string Links = "\"links\":[]";
	private const string Page = "{\"id\":\"p\",\"title\":\"T\"," + Links + "}";
	private const string SpaceJson = "{\"id\":\"s\",\"wiki\":\"w\",\"name\":\"n\",\"home\":\"h\",\"xwikiRelativeUrl\":\"r\",\"xwikiAbsoluteUrl\":\"a\"," + Links + "}";

	private sealed record Endpoint(string Path, string Json, Func<XWikiClient, CancellationToken, Task<object>> Call);

	private static readonly Dictionary<string, Endpoint> Endpoints = new()
	{
		["version"] = new("/", "{\"version\":\"16.0\",\"syntaxes\":[]," + Links + "}", async (c, t) => await c.ServerInfo.GetAsync(t)),
		["wikis"] = new("/wikis", "{\"wikis\":[{\"id\":\"xwiki\",\"name\":\"xwiki\"," + Links + "}]," + Links + "}", async (c, t) => await c.Wikis.GetWikisAsync(t)),
		["wiki"] = new("/wikis/w", "{\"id\":\"w\",\"name\":\"w\"," + Links + "}", async (c, t) => await c.Wikis.GetWikiAsync("w", t)),
		["spaces"] = new("/wikis/w/spaces", "{\"spaces\":[" + SpaceJson + "]," + Links + "}", async (c, t) => await c.Spaces.GetSpacesAsync("w", t)),
		["space"] = new("/wikis/w/spaces/s", SpaceJson, async (c, t) => await c.Spaces.GetSpaceAsync("w", "s", t)),
		["pages"] = new("/wikis/w/spaces/s/pages", "{\"pageSummaries\":[" + Page + "]," + Links + "}", async (c, t) => await c.Pages.GetPagesAsync("w", "s", t)),
		["page"] = new("/wikis/w/spaces/s/pages/p", Page, async (c, t) => await c.Pages.GetPageAsync("w", "s", "p", t)),
		["attachments"] = new("/wikis/w/spaces/s/pages/p/attachments", "{\"attachments\":[{\"id\":\"a\",\"name\":\"n\",\"mimeType\":\"text/plain\",\"size\":3," + Links + "}]," + Links + "}", async (c, t) => await c.Attachments.GetAttachmentsAsync("w", "s", "p", t)),
		["classes"] = new("/wikis/w/classes", "{\"classes\":[{\"name\":\"C\",\"properties\":[]," + Links + "}]," + Links + "}", async (c, t) => await c.Classes.GetClassesAsync("w", t)),
		["class"] = new("/wikis/w/classes/C", "{\"name\":\"C\",\"properties\":[{\"name\":\"p\",\"type\":\"String\",\"description\":null}]," + Links + "}", async (c, t) => await c.Classes.GetClassAsync("w", "C", t)),
		["comments"] = new("/wikis/w/spaces/s/pages/p/comments", "{\"comments\":[{\"id\":\"1\",\"author\":\"a\",\"text\":\"t\",\"created\":\"2026-01-02T03:04:05Z\"," + Links + "}]," + Links + "}", async (c, t) => await c.Comments.GetCommentsAsync("w", "s", "p", t)),
		["history"] = new("/wikis/w/spaces/s/pages/p/history", "{\"history\":[{\"version\":2,\"author\":\"a\",\"modified\":\"2026-01-02T03:04:05Z\",\"comment\":\"c\"}]," + Links + "}", async (c, t) => await c.History.GetHistoryAsync("w", "s", "p", t)),
		["objects"] = new("/wikis/w/spaces/s/pages/p/objects", "{\"objects\":[{\"className\":\"C\",\"number\":0,\"properties\":{},\"links\":[]}]," + Links + "}", async (c, t) => await c.Objects.GetObjectsAsync("w", "s", "p", t)),
		["object"] = new("/wikis/w/spaces/s/pages/p/objects/C/4", "{\"className\":\"C\",\"number\":4,\"properties\":{}," + Links + "}", async (c, t) => await c.Objects.GetObjectAsync("w", "s", "p", "C", 4, t)),
		["tags"] = new("/wikis/w/spaces/s/pages/p/tags", "{\"tags\":[{\"name\":\"x\"}]," + Links + "}", async (c, t) => await c.Tags.GetTagsAsync("w", "s", "p", t)),
		["search"] = new("/wikis/w/search?q=hello%20world", "{\"searchResults\":[{\"id\":\"1\",\"title\":\"T\"}]," + Links + "}", async (c, t) => await c.Search.SearchAsync("w", "hello world", t)),
	};

	/// <summary>
	/// Gets the names of the endpoints under test.
	/// </summary>
	public static TheoryData<string> EndpointNames => [.. Endpoints.Keys];

	/// <summary>
	/// Verifies each call requests the documented path with a GET and parses a response.
	/// </summary>
	[Theory]
	[MemberData(nameof(EndpointNames))]
	public async Task Call_RequestsExpectedPathAndParsesResponse(string name)
	{
		var endpoint = Endpoints[name];
		using var server = new StubXWikiServer(endpoint.Json);
		using var client = new XWikiClient(new XWikiClientOptions { Uri = server.Uri });

		var result = await endpoint.Call(client, TestContext.Current.CancellationToken);

		result.Should().NotBeNull();
		server.LastRequest!.Method.Should().Be("GET");
		server.LastRequest.PathAndQuery.Should().Be("/xwiki/rest" + endpoint.Path);
	}

	/// <summary>
	/// Verifies the parsed models carry the values from the response.
	/// </summary>
	[Fact]
	public async Task Call_MapsResponseValuesOntoModels()
	{
		using var server = new StubXWikiServer(Endpoints["history"].Json);
		using var client = new XWikiClient(new XWikiClientOptions { Uri = server.Uri });

		var result = await client.History.GetHistoryAsync("w", "s", "p", TestContext.Current.CancellationToken);

		var summary = result.History.Should().ContainSingle().Subject;
		summary.Version.Should().Be(2);
		summary.Author.Should().Be("a");
		summary.Comment.Should().Be("c");
		summary.Modified.Should().Be(new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc));
	}

	/// <summary>
	/// Verifies the render call posts its request as JSON and reads the HTML back.
	/// </summary>
	[Fact]
	public async Task Render_PostsRequestBodyAndReturnsHtml()
	{
		using var server = new StubXWikiServer("{\"html\":\"<p>hi</p>\"}");
		using var client = new XWikiClient(new XWikiClientOptions { Uri = server.Uri });

		var result = await client.Rendering.RenderAsync(
			new RenderRequest { Content = "**hi**", Syntax = "xwiki/2.1" },
			TestContext.Current.CancellationToken);

		result.Html.Should().Be("<p>hi</p>");
		server.LastRequest!.Method.Should().Be("POST");
		server.LastRequest.PathAndQuery.Should().Be("/xwiki/rest/rendering");
		server.LastRequest.Body.Should().Contain("\"content\":\"**hi**\"").And.Contain("\"syntax\":\"xwiki/2.1\"");
	}

	/// <summary>
	/// Verifies an attachment download returns the raw stream.
	/// </summary>
	[Fact]
	public async Task DownloadAttachment_ReturnsBody()
	{
		using var server = new StubXWikiServer("abc", contentType: "text/plain");
		using var client = new XWikiClient(new XWikiClientOptions { Uri = server.Uri });

		using var response = await client.Attachments.DownloadAttachmentAsync("w", "s", "p", "file.txt", TestContext.Current.CancellationToken);

		response.IsSuccessStatusCode.Should().BeTrue();
		using var reader = new StreamReader(response.Content!);
		(await reader.ReadToEndAsync(TestContext.Current.CancellationToken)).Should().Be("abc");
		server.LastRequest!.PathAndQuery.Should().Be("/xwiki/rest/wikis/w/spaces/s/pages/p/attachments/file.txt");
	}
}
