using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

public interface ISpaces
{
	/// <summary>
	/// Gets the list of spaces in a wiki.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces")]
	Task<SpacesResponse> GetSpacesAsync(string wikiId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a specific space in a wiki.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}")]
	Task<Space> GetSpaceAsync(string wikiId, string spaceKey, CancellationToken cancellationToken = default);
}
