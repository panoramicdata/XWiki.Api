namespace XWiki.Api.Models;

/// <summary>
/// Represents a Wiki.
/// </summary>
public class Wiki
{
	/// <summary>
	/// Gets Id.
	/// </summary>
	public required string Id { get; init; }
	/// <summary>
	/// Gets Name.
	/// </summary>
	public required string Name { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
