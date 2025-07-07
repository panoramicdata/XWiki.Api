using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

public interface IComments
{
	/// <summary>
	/// Gets the list of comments for a page.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}/pages/{pageName}/comments")]
	Task<CommentsResponse> GetCommentsAsync(string wikiId, string spaceKey, string pageName, CancellationToken cancellationToken = default);
}
