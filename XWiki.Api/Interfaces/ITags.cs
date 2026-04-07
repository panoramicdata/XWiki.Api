using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

/// <summary>
/// Represents a ITags.
/// </summary>
public interface ITags
{
	/// <summary>
	/// Gets the list of tags for a page.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}/pages/{pageName}/tags")]
	Task<TagsResponse> GetTagsAsync(string wikiId, string spaceKey, string pageName, CancellationToken cancellationToken);
}
