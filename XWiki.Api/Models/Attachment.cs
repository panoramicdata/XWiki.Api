namespace XWiki.Api.Models;

public class Attachment
{
	public required string Id { get; init; }
	public required string Name { get; init; }
	public required string MimeType { get; init; }
	public required long Size { get; init; }
	public required Link[] Links { get; init; }
}
