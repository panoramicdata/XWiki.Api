using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

public interface IPages
{
	/// <summary>
	/// Gets the list of pages in a space.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}/pages")]
	Task<PagesResponse> GetPagesAsync(string wikiId, string spaceKey, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a specific page.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}/pages/{pageName}")]
	Task<PageSummary> GetPageAsync(string wikiId, string spaceKey, string pageName, CancellationToken cancellationToken = default);
}
