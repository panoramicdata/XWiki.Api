namespace XWiki.Api.Models;

public class PageSummary
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required Link[] Links { get; init; }
}
