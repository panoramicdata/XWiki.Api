namespace XWiki.Api.Models;

/// <summary>
/// Represents a HistoryResponse.
/// </summary>
public class HistoryResponse
{
	/// <summary>
	/// Gets History.
	/// </summary>
	public required HistorySummary[] History { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
