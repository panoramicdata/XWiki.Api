using System.Text.Json.Serialization;

namespace XWiki.Api.Models;

public class SearchResponse
{
	[JsonPropertyName("searchResults")]
	public required SearchResult[] Results { get; init; }

	[JsonPropertyName("links")]
	public required Link[] Links { get; init; }
}
