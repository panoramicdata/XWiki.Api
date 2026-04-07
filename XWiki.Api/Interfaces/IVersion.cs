using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

/// <summary>
/// Represents a IVersion.
/// </summary>
public interface IVersion
{

	/// <summary>
	/// Gets the version of the XWiki instance.
	/// </summary>
	/// <returns>A string representing the version of the XWiki instance.</returns>
	[Get("/")]
	public Task<ServerInformation> GetAsync(CancellationToken cancellationToken);
}
