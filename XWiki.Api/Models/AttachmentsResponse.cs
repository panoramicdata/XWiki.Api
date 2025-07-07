namespace XWiki.Api.Models;

public class AttachmentsResponse
{
	public required Attachment[] Attachments { get; init; }
	public required Link[] Links { get; init; }
}
