using AwesomeAssertions;

namespace XWiki.Api.Test;

/// <summary>
/// Represents a AttachmentsTests.
/// </summary>
[Collection("Dependency Injection")]
public class AttachmentsTests(ITestOutputHelper testOutputHelper, Fixture fixture) : TestWithOutput(testOutputHelper, fixture)
{
	/// <summary>
	/// Executes GetAttachments_Succeeds.
	/// </summary>
	[Fact]
	public async Task GetAttachments_Succeeds()
	{
		var attachmentsApi = XWikiClient.Attachments;
		var context = await TryGetFirstPageContextAsync();
		if (context is null)
		{
			return;
		}

		var result = await attachmentsApi.GetAttachmentsAsync(context.Value.Wiki.Id, context.Value.Space.Id, context.Value.Page.Id, CancellationToken);
		result.Should().NotBeNull();
		result.Attachments.Should().NotBeNull();
		result.Links.Should().NotBeNull();
	}
}

