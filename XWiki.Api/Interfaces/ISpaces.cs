using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

/// <summary>
/// Represents a ISpaces.
/// </summary>
public interface ISpaces
{
	/// <summary>
	/// Gets the list of spaces in a wiki.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces")]
	Task<SpacesResponse> GetSpacesAsync(string wikiId, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific space in a wiki.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}")]
	Task<Space> GetSpaceAsync(string wikiId, string spaceKey, CancellationToken cancellationToken);
}
