using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

public interface IObjects
{
	/// <summary>
	/// Gets the list of objects for a page.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}/pages/{pageName}/objects")]
	Task<ObjectsResponse> GetObjectsAsync(string wikiId, string spaceKey, string pageName, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a specific object by class name and number.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}/pages/{pageName}/objects/{className}/{objectNumber}")]
	Task<XWikiObject> GetObjectAsync(string wikiId, string spaceKey, string pageName, string className, int objectNumber, CancellationToken cancellationToken = default);
}
