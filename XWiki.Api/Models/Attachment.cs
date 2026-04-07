namespace XWiki.Api.Models;

/// <summary>
/// Represents a Attachment.
/// </summary>
public class Attachment
{
	/// <summary>
	/// Gets Id.
	/// </summary>
	public required string Id { get; init; }
	/// <summary>
	/// Gets Name.
	/// </summary>
	public required string Name { get; init; }
	/// <summary>
	/// Gets MimeType.
	/// </summary>
	public required string MimeType { get; init; }
	/// <summary>
	/// Gets Size.
	/// </summary>
	public required long Size { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
