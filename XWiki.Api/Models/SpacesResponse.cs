namespace XWiki.Api.Models;

public class SpacesResponse
{
	public required Space[] Spaces { get; init; }

	public required Link[] Links { get; init; }
}
