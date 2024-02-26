
namespace PdfBuilder.Services;

public interface IPdfBuilderService
{
    byte[] ConvertHtmlToPdfAsync(string htmlContent);
    Task<string> GetHtmlTemplateAsync(string fileName);
    string RenderPageContent(string htmlPage, object content);
}