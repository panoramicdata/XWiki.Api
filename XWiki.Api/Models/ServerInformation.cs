namespace XWiki.Api.Models;

/// <summary>
/// Represents a ServerInformation.
/// </summary>
public class ServerInformation
{
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
	/// <summary>
	/// Gets Version.
	/// </summary>
	public required string Version { get; init; }
	/// <summary>
	/// Gets Syntaxes.
	/// </summary>
	public required Syntax[] Syntaxes { get; init; }
}
