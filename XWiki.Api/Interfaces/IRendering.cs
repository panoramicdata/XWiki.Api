using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

/// <summary>
/// Represents a IRendering.
/// </summary>
public interface IRendering
{
	/// <summary>
	/// Renders content in a given syntax.
	/// </summary>
	[Post("/rendering")]
	Task<RenderedContent> RenderAsync([Body] RenderRequest request, CancellationToken cancellationToken);
}
