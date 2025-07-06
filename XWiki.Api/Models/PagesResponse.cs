namespace XWiki.Api.Models;

public class PagesResponse
{
	public required Link[] Links { get; init; }
	public required PageSummary[] PageSummaries { get; init; }
}