namespace XWiki.Api.Models;

/// <summary>
/// Represents a HistorySummary.
/// </summary>
public class HistorySummary
{
	/// <summary>
	/// Gets Version.
	/// </summary>
	public required int Version { get; init; }
	/// <summary>
	/// Gets Author.
	/// </summary>
	public required string Author { get; init; }
	/// <summary>
	/// Gets Modified.
	/// </summary>
	public required DateTime Modified { get; init; }
	/// <summary>
	/// Gets Comment.
	/// </summary>
	public required string Comment { get; init; }
}
