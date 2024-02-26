namespace PdfBuilder.Services;

public class CalculatorReportService: ICalculatorReportService
{
    private readonly IPdfBuilderService _pdfBuilderService;
    private readonly IFileService _fileService;
    public CalculatorReportService(IPdfBuilderService pdfBuilderService, IFileService fileService)
    {
        _pdfBuilderService = pdfBuilderService;
        _fileService = fileService;
    }

    public async Task<byte[]?> CalculatorPdfAsync(CalculatorReport command)
    {

        var html = await _pdfBuilderService.GetHtmlTemplateAsync("Pdf.html");
        string? image = default;
        string? footerImage = default;
        if (!string.IsNullOrEmpty(command.ImageUrl))
        {
            var imageStream = await _fileService.DownloadImageFromUrlAsync(command.ImageUrl, default);
            image = imageStream is not null ? Convert.ToBase64String(imageStream.ToArray()) : default;
        }
        if (!string.IsNullOrEmpty(command.FooterImage))
        {
            var imageStream = await _fileService.DownloadImageFromUrlAsync(command.FooterImage, default);
            footerImage = imageStream is not null ? Convert.ToBase64String(imageStream.ToArray()) : default;
        }

        var data = new
        {
            Title = command.Title,
            CompanyName = command.CompanyName,
            PhoneNumber = command.PhoneNumber,
            Address = command.Address,
            HeaderDescription = command.HeaderDescription,
            Services = command.Services,
            ImageBase64 = image,
            ServicesAmountTitle = command.ServicesAmountTitle,
            FeaturesTitle = command.FeaturesTitle,
            Features = command.Features,
            ServiceAmountsTableHeader1 = "Overview",
            ServiceAmountsTableHeader2 = "Amount",
            ServiceAmounts = command.ServiceAmounts,
            FooterTitle = command.FooterTitle,
            FooterDescription = command.FooterDescription,
            FooterImage = footerImage,
        };

        string htmlContent = _pdfBuilderService.RenderPageContent(html, data);
        var contentFile = _pdfBuilderService.ConvertHtmlToPdfAsync(htmlContent);

        return contentFile;
    }

}
