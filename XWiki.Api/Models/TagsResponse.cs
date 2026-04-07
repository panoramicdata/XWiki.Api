namespace XWiki.Api.Models;

/// <summary>
/// Represents a TagsResponse.
/// </summary>
public class TagsResponse
{
	/// <summary>
	/// Gets Tags.
	/// </summary>
	public required Tag[] Tags { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
