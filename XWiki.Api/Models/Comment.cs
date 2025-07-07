namespace XWiki.Api.Models;

public class Comment
{
	public required string Id { get; init; }
	public required string Author { get; init; }
	public required string Text { get; init; }
	public required DateTime Created { get; init; }
	public required Link[] Links { get; init; }
}
