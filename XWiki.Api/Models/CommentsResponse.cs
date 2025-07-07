namespace XWiki.Api.Models;

public class CommentsResponse
{
	public required Comment[] Comments { get; init; }
	public required Link[] Links { get; init; }
}
