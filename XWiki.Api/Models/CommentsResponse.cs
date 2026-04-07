namespace XWiki.Api.Models;

/// <summary>
/// Represents a CommentsResponse.
/// </summary>
public class CommentsResponse
{
	/// <summary>
	/// Gets Comments.
	/// </summary>
	public required Comment[] Comments { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
