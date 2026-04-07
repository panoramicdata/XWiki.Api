namespace XWiki.Api.Models;

/// <summary>
/// Represents a XWikiObject.
/// </summary>
public class XWikiObject
{
	/// <summary>
	/// Gets ClassName.
	/// </summary>
	public required string ClassName { get; init; }
	/// <summary>
	/// Gets Number.
	/// </summary>
	public required int Number { get; init; }
	/// <summary>
	/// Gets Properties.
	/// </summary>
	public required Dictionary<string, object> Properties { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
