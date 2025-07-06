namespace XWiki.Api.Models;

public class ServerInformation
{
	public required Link[] Links { get; init; }
	public required string Version { get; init; }
	public required Syntax[] Syntaxes { get; init; }
}
