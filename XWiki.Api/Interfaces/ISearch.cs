using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

public interface ISearch
{
	/// <summary>
	/// Searches the wiki.
	/// </summary>
	[Get("/search")]
	Task<SearchResponse> SearchAsync([AliasAs("q")] string query, CancellationToken cancellationToken = default);
}
