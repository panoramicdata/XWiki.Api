namespace XWiki.Api.Models;

/// <summary>
/// Represents a XWikiClass.
/// </summary>
public class XWikiClass
{
	/// <summary>
	/// Gets Name.
	/// </summary>
	public required string Name { get; init; }
	/// <summary>
	/// Gets Properties.
	/// </summary>
	public required ClassProperty[] Properties { get; init; }
	/// <summary>
	/// Gets Links.
	/// </summary>
	public required Link[] Links { get; init; }
}

/// <summary>
/// Represents a ClassProperty.
/// </summary>
public class ClassProperty
{
	/// <summary>
	/// Gets Name.
	/// </summary>
	public required string Name { get; init; }
	/// <summary>
	/// Gets Type.
	/// </summary>
	public required string Type { get; init; }
	/// <summary>
	/// Gets Description.
	/// </summary>
	public required string? Description { get; init; }
}
