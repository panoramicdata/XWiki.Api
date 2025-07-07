namespace XWiki.Api.Models;

public class XWikiClass
{
	public required string Name { get; init; }
	public required ClassProperty[] Properties { get; init; }
	public required Link[] Links { get; init; }
}

public class ClassProperty
{
	public required string Name { get; init; }
	public required string Type { get; init; }
	public required string? Description { get; init; }
}
