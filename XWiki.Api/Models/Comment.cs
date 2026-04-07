namespace XWiki.Api.Models;

/// <summary>
/// Represents a Comment.
/// </summary>
public class Comment
{
	/// <summary>
	/// Gets Id.
	/// </summary>
	public required string Id { get; init; }
	/// <summary>
	/// Gets Author.
	/// </summary>
	public required string Author { get; init; }
	/// <summary>
	/// Gets Text.
	/// </summary>
	public required string Text { get; init; }
	/// <summary>
	/// Gets Created.
	/// </summary>
	public required DateTime Created { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
