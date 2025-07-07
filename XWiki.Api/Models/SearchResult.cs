namespace XWiki.Api.Models;

public class SearchResult
{
	public required string Id { get; init; }
	public required string Type { get; init; }
	public required string Title { get; init; }
	public string Url { get; init; }
	public required Link[] Links { get; init; }
}
