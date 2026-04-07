namespace XWiki.Api.Models;

/// <summary>
/// Represents a SpacesResponse.
/// </summary>
public class SpacesResponse
{
	/// <summary>
	/// Gets Spaces.
	/// </summary>
	public required Space[] Spaces { get; init; }

	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
