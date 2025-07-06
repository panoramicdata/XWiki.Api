namespace XWiki.Api.Test;

public class TestConfig
{
	/// <summary>
	/// Gets or sets the URI of the XWiki instance.
	/// </summary>
	public required Uri Uri { get; init; }
	/// <summary>
	/// Gets or sets the name of the wiki associated with this instance.
	/// </summary>
	public string WikiName { get; init; } = "xwiki";

	/// <summary>
	/// The username associated with the current user. Leave it null if no authentication is required.
	/// </summary>
	public string? Username { get; init; }

	/// <summary>
	/// The password associated with the current user. Leave it null if no authentication is required.
	/// </summary>
	public string? Password { get; init; }
}
