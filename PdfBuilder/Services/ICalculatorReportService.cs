namespace PdfBuilder.Services;

public interface ICalculatorReportService
{
    Task<byte[]?> CalculatorPdfAsync(CalculatorReport command);
}