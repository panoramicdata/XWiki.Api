using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

public interface IClasses
{
	/// <summary>
	/// Gets the list of classes in a wiki.
	/// </summary>
	[Get("/wikis/{wikiId}/classes")]
	Task<ClassesResponse> GetClassesAsync(string wikiId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a specific class by name.
	/// </summary>
	[Get("/wikis/{wikiId}/classes/{className}")]
	Task<XWikiClass> GetClassAsync(string wikiId, string className, CancellationToken cancellationToken = default);
}
