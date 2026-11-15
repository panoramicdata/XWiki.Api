using System.Net;
using System.Text;

namespace XWiki.Api.Test.Offline;

/// <summary>
/// A loopback HTTP server that returns one canned response and records the request it received.
/// </summary>
internal sealed class StubXWikiServer : IDisposable
{
	private readonly HttpListener _listener = new();
	private readonly Task _loop;
	private readonly int _statusCode;
	private readonly string _body;
	private readonly string _contentType;

	/// <summary>
	/// Gets the base URI of the stub, as an XWiki instance rooted at /xwiki.
	/// </summary>
	public Uri Uri { get; }

	/// <summary>
	/// Gets the most recent request, once one has arrived.
	/// </summary>
	public RecordedRequest? LastRequest { get; private set; }

	/// <summary>
	/// Starts a stub that answers every request with the supplied status and body.
	/// </summary>
	public StubXWikiServer(string body, int statusCode = 200, string contentType = "application/json")
	{
		_body = body;
		_statusCode = statusCode;
		_contentType = contentType;

		var port = GetFreePort();
		var prefix = $"http://127.0.0.1:{port}/";
		_listener.Prefixes.Add(prefix);
		_listener.Start();
		Uri = new Uri(prefix + "xwiki");
		_loop = Task.Run(ServeAsync);
	}

	private static int GetFreePort()
	{
		var probe = new System.Net.Sockets.TcpListener(IPAddress.Loopback, 0);
		probe.Start();
		var port = ((IPEndPoint)probe.LocalEndpoint).Port;
		probe.Stop();
		return port;
	}

	private async Task ServeAsync()
	{
		while (_listener.IsListening)
		{
			HttpListenerContext context;
			try
			{
				context = await _listener.GetContextAsync();
			}
			catch (Exception exception) when (exception is HttpListenerException or ObjectDisposedException)
			{
				return;
			}

			using var reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
			var requestBody = await reader.ReadToEndAsync();
			LastRequest = new RecordedRequest(
				context.Request.HttpMethod,
				context.Request.Url!.PathAndQuery,
				context.Request.Headers["Authorization"],
				context.Request.Headers["User-Agent"],
				context.Request.Headers["Accept"],
				requestBody);

			var bytes = Encoding.UTF8.GetBytes(_body);
			context.Response.StatusCode = _statusCode;
			context.Response.ContentType = _contentType;
			await context.Response.OutputStream.WriteAsync(bytes);
			context.Response.Close();
		}
	}

	/// <summary>
	/// Stops the server.
	/// </summary>
	public void Dispose()
	{
		_listener.Close();
		_loop.GetAwaiter().GetResult();
	}
}

/// <summary>
/// The parts of an HTTP request the tests assert on.
/// </summary>
internal sealed record RecordedRequest(
	string Method,
	string PathAndQuery,
	string? Authorization,
	string? UserAgent,
	string? Accept,
	string Body);
