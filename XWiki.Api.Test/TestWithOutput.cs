
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XWiki.Api.Models;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace XWiki.Api.Test;

/// <summary>
/// Represents this member.
/// </summary>
[CollectionDefinition("Dependency Injection")]
public abstract class TestWithOutput : TestBed<Fixture>
{
	/// <summary>
	/// Gets Logger.
	/// </summary>
	protected ILogger Logger { get; }

	/// <summary>
	/// Gets XWikiClient.
	/// </summary>
	protected XWikiClient XWikiClient { get; }

	private protected TestConfig TestConfig { get; }

	/// <summary>
	/// Represents this member.
	/// </summary>
	protected static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

	/// <summary>
	/// Initializes a new instance.
	/// </summary>
	protected TestWithOutput(ITestOutputHelper testOutputHelper, Fixture fixture) : base(testOutputHelper, fixture)
	{
		ArgumentNullException.ThrowIfNull(testOutputHelper);
		ArgumentNullException.ThrowIfNull(fixture);

		// Logger
		var loggerFactory = fixture.GetService<ILoggerFactory>(testOutputHelper) ?? throw new InvalidOperationException("LoggerFactory is null");
		Logger = loggerFactory.CreateLogger(GetType());

		// TestPortalConfig
		var testPortalConfigOptions = fixture
			.GetService<IOptions<TestConfig>>(testOutputHelper)
			?? throw new InvalidOperationException("TestPortalConfig is null");

		TestConfig = testPortalConfigOptions.Value;

		XWikiClient = new XWikiClient(new XWikiClientOptions
		{
			Uri = TestConfig.Uri,
			Username = TestConfig.Username,
			Password = TestConfig.Password,
			Logger = Logger
		});
	}

	private protected async Task<(Wiki Wiki, Space Space, PageSummary Page)?> TryGetFirstPageContextAsync()
	{
		var wikis = await XWikiClient.Wikis.GetWikisAsync(CancellationToken);
		foreach (var wiki in wikis.Wikis)
		{
			var spaces = await XWikiClient.Spaces.GetSpacesAsync(wiki.Id, CancellationToken);
			foreach (var space in spaces.Spaces)
			{
				var pages = await XWikiClient.Pages.GetPagesAsync(wiki.Id, space.Id, CancellationToken);
				var page = pages.PageSummaries.FirstOrDefault();
				if (page is not null)
				{
					return (wiki, space, page);
				}
			}
		}

		return null;
	}
}

