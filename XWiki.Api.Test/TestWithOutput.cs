using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace XWiki.Api.Test;

[CollectionDefinition("Dependency Injection")]
public abstract class TestWithOutput : TestBed<Fixture>
{
	protected ILogger Logger { get; }

	protected XWikiClient XWikiClient { get; }

	public TestWithOutput(ITestOutputHelper testOutputHelper, Fixture fixture) : base(testOutputHelper, fixture)
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

		var testPortalConfig = testPortalConfigOptions.Value;

		XWikiClient = new XWikiClient(new XWikiClientOptions
		{
			Uri = testPortalConfig.Uri,
			WikiName = testPortalConfig.WikiName,
			Username = testPortalConfig.Username,
			Password = testPortalConfig.Password,
			Logger = Logger
		});
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}
}
