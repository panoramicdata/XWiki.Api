namespace XWiki.Api.Models;

public class ObjectsResponse
{
	public required XWikiObject[] Objects { get; init; }
	public required Link[] Links { get; init; }
}
