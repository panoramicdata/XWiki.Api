namespace XWiki.Api.Models;

public class Space
{
	public required Link[] Links { get; set; }
	public required string Id { get; set; }
	public required string Wiki { get; set; }
	public required string Name { get; set; }
	public required string Home { get; set; }
	public required string XwikiRelativeUrl { get; set; }
	public required string XwikiAbsoluteUrl { get; set; }
}
