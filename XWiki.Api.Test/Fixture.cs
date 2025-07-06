using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace XWiki.Api.Test;

public class Fixture : TestBedFixture
{
	private IConfigurationRoot? _configuration;

	protected override void AddServices(
		IServiceCollection services,
		IConfiguration? configuration)
	{
		if (_configuration is null)
		{
			throw new InvalidOperationException("Configuration is null");
		}

		services
			.AddScoped<CancellationTokenSource>()
			.Configure<TestConfig>(_configuration.GetSection("Config"));

		// Add logging with Debug level and providers for Console and Debug output
		services.AddLogging(builder =>
		{
			builder.SetMinimumLevel(LogLevel.Debug);
			builder.AddDebug();
		});
	}

	protected override ValueTask DisposeAsyncCore()
		=> default;

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
