namespace XWiki.Api.Models;

public class ClassesResponse
{
	public required XWikiClass[] Classes { get; init; }
	public required Link[] Links { get; init; }
}
