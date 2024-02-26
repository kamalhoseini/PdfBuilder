using Microsoft.AspNetCore.Mvc;
using PdfBuilder.Services;

namespace PdfBuilder.Controllers;
[ApiController]
[Route("[controller]")]
public class HtmlToPdfController : ControllerBase
{
    private readonly ICalculatorReportService _calculatorReportService;

    public HtmlToPdfController(ICalculatorReportService calculatorReportService)
    {
        _calculatorReportService = calculatorReportService;
    }


    [HttpPost(Name = "Generate-Pdf")]
    public async Task<FileContentResult> GeneratePdf([FromBody] CalculatorReport command)
    {
        var contentFile = await _calculatorReportService.CalculatorPdfAsync(command);

        if (contentFile is null or { Length: 0 })
            return default!;

        FileContentResult file = new FileContentResult(contentFile, "application/pdf")
        {
            FileDownloadName = $"{DateTime.UtcNow.ToString("yyyy/MM/dd-hh:mm:ss")}.pdf"
        };


        return file;
    }

}
