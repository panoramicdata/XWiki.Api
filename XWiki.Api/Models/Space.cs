namespace XWiki.Api.Models;

/// <summary>
/// Represents a Space.
/// </summary>
public class Space
{
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; set; }
	/// <summary>
	/// Gets Id.
	/// </summary>
	public required string Id { get; set; }
	/// <summary>
	/// Gets Wiki.
	/// </summary>
	public required string Wiki { get; set; }
	/// <summary>
	/// Gets Name.
	/// </summary>
	public required string Name { get; set; }
	/// <summary>
	/// Gets Home.
	/// </summary>
	public required string Home { get; set; }
	/// <summary>
	/// Gets XwikiRelativeUrl.
	/// </summary>
	public required string XwikiRelativeUrl { get; set; }
	/// <summary>
	/// Gets XwikiAbsoluteUrl.
	/// </summary>
	public required string XwikiAbsoluteUrl { get; set; }
}
