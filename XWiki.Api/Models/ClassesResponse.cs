namespace XWiki.Api.Models;

/// <summary>
/// Represents a ClassesResponse.
/// </summary>
public class ClassesResponse
{
	/// <summary>
	/// Gets Classes.
	/// </summary>
	public XWikiClass[] Classes { get; init; } = [];
	/// <summary>
	/// Gets Links.
	/// </summary>
	public Link[] Links { get; init; } = [];
}
