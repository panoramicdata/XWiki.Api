

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace XWiki.Api;

internal sealed class LoggingHttpClientHandler : HttpClientHandler
{
	private readonly ILogger _logger;

	public LoggingHttpClientHandler(XWikiClientOptions xWikiClientOptions)
	{
		ArgumentNullException.ThrowIfNull(xWikiClientOptions, nameof(xWikiClientOptions));
		// Initialize the logger if provided, otherwise use a default logger.
		_logger = xWikiClientOptions.Logger ?? NullLogger.Instance;

		// Additional initialization can be done here if needed.
		_logger.LogDebug("LoggingHttpClientHandler initialized with base URI: {BaseUri}", xWikiClientOptions.Uri);
	}

	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request, nameof(request));

		// If we are debugging, log the request details
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			_logger.LogDebug("Sending request to {RequestUri} with method {Method}", request.RequestUri, request.Method);
			if (request.Content != null)
			{
				var content = await request.Content.ReadAsStringAsync(cancellationToken);
				_logger.LogDebug("Request content: {Content}", content);
			}
		}

		// Get the response from the base handler
		var response = await base.SendAsync(request, cancellationToken);

		// Log the response status code
		_logger.LogDebug("Received response with status code: {StatusCode}", response.StatusCode);

		// Optionally, you can log the response content if needed
		if (response.IsSuccessStatusCode)
		{
			_logger.LogDebug("Request to {RequestUri} succeeded with status code: {StatusCode}", request.RequestUri, response.StatusCode);
		}
		else
		{
			_logger.LogWarning("Request to {RequestUri} failed with status code: {StatusCode}", request.RequestUri, response.StatusCode);
		}

		// If we are debugging, log the response content
		if (_logger.IsEnabled(LogLevel.Debug) && response.Content is not null)
		{
			var content = await response.Content.ReadAsStringAsync(cancellationToken);
			_logger.LogDebug("Response content: {Content}", content);
		}

		// Return the response
		return response;
	}
}