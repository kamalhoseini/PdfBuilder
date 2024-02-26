using HandlebarsDotNet;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using System.Reflection;

namespace PdfBuilder.Services;

public class PdfBuilderService : IPdfBuilderService
{
    public PdfBuilderService()
    {
    }

    public async Task<string> GetHtmlTemplateAsync(string fileName)
    {
        var filename = fileName;
        var assembly = Assembly.GetExecutingAssembly();
        var template = assembly.GetManifestResourceNames()
            .FirstOrDefault(file => file.Contains(filename, StringComparison.OrdinalIgnoreCase));
        using Stream stream = assembly.GetManifestResourceStream(template!)!;
        using StreamReader reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
    public string RenderPageContent(string htmlPage, object content)
    {
        var template = Handlebars.Compile(htmlPage);
        return template(content);
    }

    public byte[] ConvertHtmlToPdfAsync(string htmlContent)
    {
        ConverterProperties converterProperties = new ConverterProperties();

        var outputStream = new MemoryStream();
        PdfWriter writer = new PdfWriter(outputStream);
        PdfDocument pdf = new PdfDocument(writer);
        pdf.SetDefaultPageSize(PageSize.A4);
        var document = HtmlConverter.ConvertToDocument(htmlContent, pdf, converterProperties);
        document.Close();
        return outputStream.ToArray();
    }
}