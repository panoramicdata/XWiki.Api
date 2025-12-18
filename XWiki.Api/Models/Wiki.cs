namespace XWiki.Api.Models;

public class Wiki
{
	public required string Id { get; init; }
	public required string Name { get; init; }
	public required Link[] Links { get; init; }
}
