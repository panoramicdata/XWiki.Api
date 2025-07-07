using Refit;
using XWiki.Api.Models;

namespace XWiki.Api.Interfaces;

public interface IAttachments
{
	/// <summary>
	/// Gets the list of attachments for a page.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}/pages/{pageName}/attachments")]
	Task<AttachmentsResponse> GetAttachmentsAsync(string wikiId, string spaceKey, string pageName, CancellationToken cancellationToken = default);

	/// <summary>
	/// Downloads a specific attachment.
	/// </summary>
	[Get("/wikis/{wikiId}/spaces/{spaceKey}/pages/{pageName}/attachments/{attachmentName}")]
	Task<ApiResponse<Stream>> DownloadAttachmentAsync(string wikiId, string spaceKey, string pageName, string attachmentName, CancellationToken cancellationToken = default);
}
