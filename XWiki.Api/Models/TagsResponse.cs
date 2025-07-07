namespace XWiki.Api.Models;

public class TagsResponse
{
	public required Tag[] Tags { get; init; }
	public required Link[] Links { get; init; }
}
