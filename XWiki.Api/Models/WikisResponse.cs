namespace XWiki.Api.Models;

public class WikisResponse
{
	public required Wiki[] Wikis { get; init; }
	public required Link[] Links { get; init; }
}
