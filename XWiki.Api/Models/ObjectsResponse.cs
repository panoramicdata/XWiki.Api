namespace XWiki.Api.Models;

/// <summary>
/// Represents a ObjectsResponse.
/// </summary>
public class ObjectsResponse
{
	/// <summary>
	/// Gets Objects.
	/// </summary>
	public required XWikiObject[] Objects { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
