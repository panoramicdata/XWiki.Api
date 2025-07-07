namespace XWiki.Api.Models;

public class HistorySummary
{
	public required int Version { get; init; }
	public required string Author { get; init; }
	public required DateTime Modified { get; init; }
	public required string Comment { get; init; }
}
