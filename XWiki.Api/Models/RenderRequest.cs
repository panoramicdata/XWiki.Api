namespace XWiki.Api.Models;

public class RenderRequest
{
	public required string Content { get; init; }
	public required string Syntax { get; init; }
}

public class RenderedContent
{
	public required string Html { get; init; }
}
