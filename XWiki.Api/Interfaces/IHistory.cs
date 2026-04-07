using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

/// <summary>
/// Represents a IHistory.
/// </summary>
public interface IHistory
{
	/// <summary>
	/// Gets the history of a page.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}/pages/{pageName}/history")]
	Task<HistoryResponse> GetHistoryAsync(string wikiId, string spaceKey, string pageName, CancellationToken cancellationToken);
}
