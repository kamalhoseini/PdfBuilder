namespace PdfBuilder.Services;

public interface IFileService
{
    Task<MemoryStream?> DownloadImageFromUrlAsync(string url, CancellationToken cancellationToken);
}