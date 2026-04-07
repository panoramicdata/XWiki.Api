using System.Text.Json.Serialization;

namespace XWiki.Api.Models;

/// <summary>
/// Represents a SearchResponse.
/// </summary>
public class SearchResponse
{
	/// <summary>
	/// Gets Results.
	/// </summary>
	[JsonPropertyName("searchResults")]
	public required SearchResult[] Results { get; init; }

	/// <summary>
	/// Gets Links.
	/// </summary>
	[JsonPropertyName("links")]
	public required Link[] Links { get; init; }
}
