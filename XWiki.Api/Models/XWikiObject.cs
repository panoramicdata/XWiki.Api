namespace XWiki.Api.Models;

public class XWikiObject
{
	public required string ClassName { get; init; }
	public required int Number { get; init; }
	public required Dictionary<string, object> Properties { get; init; }
	public required Link[] Links { get; init; }
}
