namespace XWiki.Api.Models;

/// <summary>
/// Represents a WikisResponse.
/// </summary>
public class WikisResponse
{
	/// <summary>
	/// Gets Wikis.
	/// </summary>
	public required Wiki[] Wikis { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
