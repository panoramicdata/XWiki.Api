namespace XWiki.Api.Models;

/// <summary>
/// Represents a PageSummary.
/// </summary>
public class PageSummary
{
	/// <summary>
	/// Gets Id.
	/// </summary>
	public required string Id { get; init; }
	/// <summary>
	/// Gets Title.
	/// </summary>
	public required string Title { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
