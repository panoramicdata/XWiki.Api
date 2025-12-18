namespace XWiki.Api.Models;

public class Syntax
{
	public required string Id { get; init; }
	public required string Name { get; init; }
	public required string Version { get; init; }
	public required Link[] Links { get; init; }
}