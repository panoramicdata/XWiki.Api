namespace XWiki.Api.Models;

public class HistoryResponse
{
	public required HistorySummary[] History { get; init; }
	public required Link[] Links { get; init; }
}
