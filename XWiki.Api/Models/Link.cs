namespace XWiki.Api.Models;

/// <summary>
/// Represents a Link.
/// </summary>
public class Link
{
	/// <summary>
	/// Gets Href.
	/// </summary>
	public required string Href { get; init; }

	/// <summary>
	/// Gets Rel.
	/// </summary>
	public required string Rel { get; init; }

	/// <summary>
	/// Gets Type.
	/// </summary>
	public required string? Type { get; init; }

	/// <summary>
	/// Gets HrefLang.
	/// </summary>
	public required string? HrefLang { get; init; }
}
