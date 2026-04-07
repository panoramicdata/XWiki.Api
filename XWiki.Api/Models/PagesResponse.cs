namespace XWiki.Api.Models;

/// <summary>
/// Represents a PagesResponse.
/// </summary>
public class PagesResponse
{
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
	/// <summary>
	/// Gets PageSummaries.
	/// </summary>
	public required PageSummary[] PageSummaries { get; init; }
}
