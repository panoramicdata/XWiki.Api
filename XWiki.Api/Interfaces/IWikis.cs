using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

public interface IWikis
{
	/// <summary>
	/// Gets the list of wikis.
	/// </summary>
	[Get("/wikis")]
	Task<WikisResponse> GetWikisAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a specific wiki by its id.
	/// </summary>
	[Get("/wikis/{wikiId}")]
	Task<Wiki> GetWikiAsync(string wikiId, CancellationToken cancellationToken = default);
}
