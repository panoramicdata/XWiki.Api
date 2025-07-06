

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http.Headers;
using System.Text;

namespace XWiki.Api;

internal class CustomHttpMessageHandler : HttpClientHandler
{
	private readonly ILogger _logger;
	private readonly string? _username;
	private readonly string? _password;

	public CustomHttpMessageHandler(XWikiClientOptions xWikiClientOptions)
	{
		ArgumentNullException.ThrowIfNull(xWikiClientOptions, nameof(xWikiClientOptions));
		// Initialize the logger if provided, otherwise use a default logger.
		_logger = xWikiClientOptions.Logger ?? NullLogger.Instance;

		// Additional initialization can be done here if needed.
		_logger.LogDebug("CustomHttpMessageHandler initialized with base URI: {BaseUri}", xWikiClientOptions.Uri);

		_username = xWikiClientOptions.Username;
		_password = xWikiClientOptions.Password;
	}

	protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
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

		// If username and password are provided, add them to the request headers
		if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password))
		{
			var byteArray = Encoding.ASCII.GetBytes($"{_username}:{_password}");
			request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
			_logger.LogDebug("Added Basic Authentication header to the request.");
		}
		else
		{
			_logger.LogDebug("No authentication headers added to the request.");
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
		if (_logger.IsEnabled(LogLevel.Debug) && response.Content != null)
		{
			var content = await response.Content.ReadAsStringAsync(cancellationToken);
			_logger.LogDebug("Response content: {Content}", content);
		}

		// Return the response
		return response;
	}
}