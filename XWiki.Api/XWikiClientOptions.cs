using Microsoft.Extensions.Logging;

namespace XWiki.Api;

/// <summary>
/// Represents configuration options for connecting to an XWiki instance.
/// </summary>
/// <remarks>This class is used to specify the settings required to establish a connection to an XWiki instance.
/// It includes the URI of the XWiki server, which is a mandatory property.</remarks>
public class XWikiClientOptions
{
	/// <summary>
	/// Gets the URI of the XWiki instance.
	/// </summary>
	/// </summary>
	public required Uri Uri { get; init; }

	/// <summary>
	/// Gets the name of the wiki associated with this instance.
	/// </summary>
	public string WikiName { get; init; } = "xwiki";

	/// <summary>
	/// The logger to be used for logging within the XWiki client.`
	///	</summary>
	public ILogger? Logger { get; init; } = null!;

	/// <summary>
	/// Gets the username associated with the current user.  Leave it null if no authentication is required.
	/// </summary>
	public string? Username { get; init; }

	/// <summary>
	/// Gets the password associated with the current user.  Leave it null if no authentication is required.
	/// </summary>
	public string? Password { get; init; }
}