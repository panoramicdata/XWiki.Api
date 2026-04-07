namespace XWiki.Api.Models;

/// <summary>
/// Represents a RenderRequest.
/// </summary>
public class RenderRequest
{
	/// <summary>
	/// Gets Content.
	/// </summary>
	public required string Content { get; init; }
	/// <summary>
	/// Gets Syntax.
	/// </summary>
	public required string Syntax { get; init; }
}

/// <summary>
/// Represents a RenderedContent.
/// </summary>
public class RenderedContent
{
	/// <summary>
	/// Gets Html.
	/// </summary>
	public required string Html { get; init; }
}
