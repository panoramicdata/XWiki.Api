
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace XWiki.Api.Test;

[CollectionDefinition("Dependency Injection")]
public abstract class TestWithOutput : TestBed<Fixture>
{
	protected ILogger Logger { get; }

	protected XWikiClient XWikiClient { get; }

	private protected TestConfig TestConfig { get; }

	protected static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

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

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}
}
