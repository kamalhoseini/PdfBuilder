namespace PdfBuilder.Services;

public class FileService : IFileService
{
    public async Task<MemoryStream?> DownloadImageFromUrlAsync(string url, CancellationToken cancellationToken)
    {
        url = url.StartsWith("http") ? url : $"https:{url}";
        using (HttpClient client = new HttpClient())
        {
            using (Stream stream = await client.GetStreamAsync(url, cancellationToken))
            {
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            }
        }
    }

}



