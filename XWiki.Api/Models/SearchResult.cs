namespace XWiki.Api.Models;

/// <summary>
/// Represents a SearchResult.
/// </summary>
public class SearchResult
{
	/// <summary>
	/// Gets Id.
	/// </summary>
	public string Id { get; init; } = string.Empty;
	/// <summary>
	/// Gets Type.
	/// </summary>
	public string Type { get; init; } = string.Empty;
	/// <summary>
	/// Gets Title.
	/// </summary>
	public string Title { get; init; } = string.Empty;
	/// <summary>
	/// Gets Url.
	/// </summary>
	public string Url { get; init; } = string.Empty;
	/// <summary>
	/// Gets Links.
	/// </summary>
	public Link[] Links { get; init; } = [];
}
