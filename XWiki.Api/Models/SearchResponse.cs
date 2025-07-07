namespace XWiki.Api.Models;

public class SearchResult
{
	public required string Id { get; init; }
	public required string Type { get; init; }
	public required string Title { get; init; }
	public required string Url { get; init; }
	public required Link[] Links { get; init; }
}

public class SearchResponse
{
	public required SearchResult[] Results { get; init; }
	public required Link[] Links { get; init; }
}
