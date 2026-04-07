using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a Fixture.
/// </summary>
public class Fixture : TestBedFixture
{
	private IConfigurationRoot? _configuration;

	/// <summary>
	/// Executes AddServices.
	/// </summary>
	protected override void AddServices(
		IServiceCollection services,
		IConfiguration? configuration)
	{
		if (_configuration is null)
		{
			throw new InvalidOperationException("Configuration is null");
		}

		_ = services
			.AddScoped<CancellationTokenSource>()
			.Configure<TestConfig>(_configuration.GetSection("Config"));

		// Add logging with Debug level and providers for Console and Debug output
		_ = services.AddLogging(builder => _ = builder.SetMinimumLevel(LogLevel.Debug)
			.AddDebug());
	}

	/// <summary>
	/// Executes DisposeAsyncCore.
	/// </summary>
	protected override ValueTask DisposeAsyncCore()
		=> default;

	/// <summary>
	/// Executes GetTestAppSettings.
	/// </summary>
	protected override IEnumerable<TestAppSettings> GetTestAppSettings()
	{
		_configuration = new ConfigurationBuilder()
			 .SetBasePath(Directory.GetCurrentDirectory())
			 .AddUserSecrets<Fixture>()
			 .Build();

		// This is not used
		return [
			new TestAppSettings
			{
				IsOptional = true,
				Filename = null,
			}
		];
	}
}
