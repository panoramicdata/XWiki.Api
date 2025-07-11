using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

public interface ISearch
{
	/// <summary>
	/// Searches the wiki.
	/// </summary>
	[Get("/wikis/{wikiName}/search")]
	Task<SearchResponse> SearchAsync(string wikiName, [AliasAs("q")] string query, CancellationToken cancellationToken = default);
}
