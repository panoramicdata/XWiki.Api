using Refit;
using System.Net.Http.Headers;
using System.Text;
using XWiki.Api.Interfaces;

namespace XWiki.Api;

/// <summary>
/// A client for interacting with XWiki.
/// </summary>
public class XWikiClient : IDisposable
{
	private bool disposedValue;
	private readonly HttpClient _httpClient;

	/// <summary>
	/// Initializes a new instance.
	/// </summary>
	public XWikiClient(XWikiClientOptions xWikiClientOptions)
	{
		ArgumentNullException.ThrowIfNull(xWikiClientOptions, nameof(xWikiClientOptions));

		// The base url might look like this: https://xwiki.example.com/
		// The options should contain the path of the XWiki instance.
		// So for example , if the XWiki instance is hosted at https://xwiki.example.com/xwiki,the base address should be set to https://xwiki.example.com/xwiki/

		// Set base address for the HttpClient to this combined path.
		var baseAddress = new Uri(xWikiClientOptions.Uri.ToString().TrimEnd('/') + "/rest");

		var httpMessageHandler = new LoggingHttpClientHandler(xWikiClientOptions);

		_httpClient = new HttpClient(httpMessageHandler)
		{
			BaseAddress = baseAddress
		};

		_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
		_httpClient.DefaultRequestHeaders.Add("User-Agent", xWikiClientOptions.UserAgent);

		// Add authentication headers if username and password are provided
		if (!string.IsNullOrEmpty(xWikiClientOptions.Username) && !string.IsNullOrEmpty(xWikiClientOptions.Password))
		{
			var byteArray = Encoding.ASCII.GetBytes($"{xWikiClientOptions.Username}:{xWikiClientOptions.Password}");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
		}

		var refitSettings = new RefitSettings
		{
			ContentSerializer = new SystemTextJsonContentSerializer()
		};

		ServerInfo = RestService.For<IVersion>(_httpClient, refitSettings);
		Wikis = RestService.For<IWikis>(_httpClient, refitSettings);
		Spaces = RestService.For<ISpaces>(_httpClient, refitSettings);
		Pages = RestService.For<IPages>(_httpClient, refitSettings);
		Attachments = RestService.For<IAttachments>(_httpClient, refitSettings);
		Objects = RestService.For<IObjects>(_httpClient, refitSettings);
		Classes = RestService.For<IClasses>(_httpClient, refitSettings);
		Comments = RestService.For<IComments>(_httpClient, refitSettings);
		Tags = RestService.For<ITags>(_httpClient, refitSettings);
		History = RestService.For<IHistory>(_httpClient, refitSettings);
		Rendering = RestService.For<IRendering>(_httpClient, refitSettings);
		Search = RestService.For<ISearch>(_httpClient, refitSettings);
	}

	/// <summary>
	/// Gets ServerInfo.
	/// </summary>
	public IVersion ServerInfo { get; init; }

	/// <summary>
	/// Gets Wikis.
	/// </summary>
	public IWikis Wikis { get; }
	/// <summary>
	/// Gets Spaces.
	/// </summary>
	public ISpaces Spaces { get; }
	/// <summary>
	/// Gets Pages.
	/// </summary>
	public IPages Pages { get; }
	/// <summary>
	/// Gets Attachments.
	/// </summary>
	public IAttachments Attachments { get; }
	/// <summary>
	/// Gets Objects.
	/// </summary>
	public IObjects Objects { get; }
	/// <summary>
	/// Gets Classes.
	/// </summary>
	public IClasses Classes { get; }
	/// <summary>
	/// Gets Comments.
	/// </summary>
	public IComments Comments { get; }
	/// <summary>
	/// Gets Tags.
	/// </summary>
	public ITags Tags { get; }
	/// <summary>
	/// Gets History.
	/// </summary>
	public IHistory History { get; }
	/// <summary>
	/// Gets Rendering.
	/// </summary>
	public IRendering Rendering { get; }
	/// <summary>
	/// Gets Search.
	/// </summary>
	public ISearch Search { get; }

	/// <summary>
	/// Executes Dispose.
	/// </summary>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_httpClient.Dispose();
			}

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			disposedValue = true;
		}
	}

	/// <summary>
	/// Executes Dispose.
	/// </summary>
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
