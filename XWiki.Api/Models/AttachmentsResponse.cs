namespace XWiki.Api.Models;

/// <summary>
/// Represents a AttachmentsResponse.
/// </summary>
public class AttachmentsResponse
{
	/// <summary>
	/// Gets Attachments.
	/// </summary>
	public required Attachment[] Attachments { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}
