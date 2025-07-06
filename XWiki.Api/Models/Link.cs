namespace XWiki.Api.Models;

public class Link
{
	public required string Href { get; init; }

	public required string Rel { get; init; }

	public required string? Type { get; init; }

	public required string? HrefLang { get; init; }
}
