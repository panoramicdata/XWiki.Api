using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

/// <summary>
/// Represents a IClasses.
/// </summary>
public interface IClasses
{
	/// <summary>
	/// Gets the list of classes in a wiki.
	/// </summary>
	[Get("/wikis/{wikiId}/classes")]
	Task<ClassesResponse> GetClassesAsync(string wikiId, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific class by name.
	/// </summary>
	[Get("/wikis/{wikiId}/classes/{className}")]
	Task<XWikiClass> GetClassAsync(string wikiId, string className, CancellationToken cancellationToken);
}
