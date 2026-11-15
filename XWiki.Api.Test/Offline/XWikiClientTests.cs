using AwesomeAssertions;
using Microsoft.Extensions.Logging;
using Refit;

namespace XWiki.Api.Test.Offline;

/// <summary>
/// Offline tests for client construction, authentication headers, logging and failure handling.
/// </summary>
public class XWikiClientTests
{
	private const string VersionJson = "{\"version\":\"16.0\",\"syntaxes\":[],\"links\":[]}";

	/// <summary>
	/// Verifies a null options object is rejected.
	/// </summary>
	[Fact]
	public void Constructor_WithNullOptions_Throws()
	{
		var act = () => new XWikiClient(null!);

		act.Should().Throw<ArgumentNullException>();
	}

	/// <summary>
	/// Verifies the default user agent and the JSON accept header are sent.
	/// </summary>
	[Fact]
	public async Task Request_SendsDefaultHeaders()
	{
		using var server = new StubXWikiServer(VersionJson);
		using var client = new XWikiClient(new XWikiClientOptions { Uri = server.Uri });

		await client.ServerInfo.GetAsync(TestContext.Current.CancellationToken);

		server.LastRequest!.UserAgent.Should().Be("XWiki.Api/1.0.0");
		server.LastRequest.Accept.Should().Be("application/json");
		server.LastRequest.Authorization.Should().BeNull();
	}

	/// <summary>
	/// Verifies basic authentication is sent only when both a username and a password are supplied.
	/// </summary>
	[Theory]
	[InlineData("user", "secret", "Basic dXNlcjpzZWNyZXQ=")]
	[InlineData("user", null, null)]
	[InlineData(null, "secret", null)]
	[InlineData("", "", null)]
	public async Task Request_SendsBasicAuthenticationOnlyWithBothParts(string? username, string? password, string? expectedHeader)
	{
		using var server = new StubXWikiServer(VersionJson);
		using var client = new XWikiClient(new XWikiClientOptions { Uri = server.Uri, Username = username, Password = password, UserAgent = "custom/2.0" });

		await client.ServerInfo.GetAsync(TestContext.Current.CancellationToken);

		server.LastRequest!.Authorization.Should().Be(expectedHeader);
		server.LastRequest.UserAgent.Should().Be("custom/2.0");
	}

	/// <summary>
	/// Verifies a trailing slash on the instance URI does not produce a double slash.
	/// </summary>
	[Fact]
	public async Task Request_WithTrailingSlashOnUri_BuildsCleanPath()
	{
		using var server = new StubXWikiServer(VersionJson);
		using var client = new XWikiClient(new XWikiClientOptions { Uri = new Uri(server.Uri + "/") });

		await client.ServerInfo.GetAsync(TestContext.Current.CancellationToken);

		server.LastRequest!.PathAndQuery.Should().Be("/xwiki/rest/");
	}

	/// <summary>
	/// Verifies the handler logs the request and response at debug level and a success without a warning.
	/// </summary>
	[Fact]
	public async Task Request_LogsRequestAndResponseAtDebug()
	{
		var logger = new CapturingLogger();
		using var server = new StubXWikiServer(VersionJson);
		using var client = new XWikiClient(new XWikiClientOptions { Uri = server.Uri, Logger = logger });

		await client.ServerInfo.GetAsync(TestContext.Current.CancellationToken);

		var messages = logger.Entries.Select(entry => entry.Message).ToList();
		messages.Should().Contain(message => message.Contains("initialized with base URI"));
		messages.Should().Contain(message => message.Contains("Sending request to") && message.Contains("GET"));
		messages.Should().Contain(message => message.Contains("Response content:") && message.Contains("16.0"));
		logger.Entries.Should().NotContain(entry => entry.Level == LogLevel.Warning);
	}

	/// <summary>
	/// Verifies the request body is logged when the call has one.
	/// </summary>
	[Fact]
	public async Task Request_WithBody_LogsContent()
	{
		var logger = new CapturingLogger();
		using var server = new StubXWikiServer("{\"html\":\"\"}");
		using var client = new XWikiClient(new XWikiClientOptions { Uri = server.Uri, Logger = logger });

		await client.Rendering.RenderAsync(new Models.RenderRequest { Content = "body-text", Syntax = "plain/1.0" }, TestContext.Current.CancellationToken);

		logger.Entries.Should().Contain(entry => entry.Message.Contains("Request content:") && entry.Message.Contains("body-text"));
	}

	/// <summary>
	/// Verifies a failure status is logged as a warning and surfaced as a Refit exception.
	/// </summary>
	[Fact]
	public async Task Request_WithFailureStatus_LogsWarningAndThrows()
	{
		var logger = new CapturingLogger();
		using var server = new StubXWikiServer("{}", statusCode: 404);
		using var client = new XWikiClient(new XWikiClientOptions { Uri = server.Uri, Logger = logger });

		var act = () => client.Wikis.GetWikiAsync("missing", TestContext.Current.CancellationToken);

		var exception = await act.Should().ThrowAsync<ApiException>();
		exception.Which.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
		logger.Entries.Should().Contain(entry => entry.Level == LogLevel.Warning && entry.Message.Contains("failed with status code"));
	}

	/// <summary>
	/// Verifies disposing the client more than once is harmless.
	/// </summary>
	[Fact]
	public void Dispose_CanBeCalledTwice()
	{
		var client = new XWikiClient(new XWikiClientOptions { Uri = new Uri("http://127.0.0.1:1/xwiki") });

		client.Dispose();
		var act = client.Dispose;

		act.Should().NotThrow();
	}
}
