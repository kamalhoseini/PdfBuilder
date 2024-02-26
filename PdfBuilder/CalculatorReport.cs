namespace PdfBuilder;

public class CalculatorReport
{
    public string? Title { get; set; }
    public string? CompanyName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? HeaderDescription { get; set; }
    public IEnumerable<string>? Services { get; set; }
    public string? FeaturesTitle { get; set; }
    public List<Feature>? Features { get; set; }
    public string? ImageUrl { get; set; }
    public string? ServicesAmountTitle { get; set; }
    public List<ServiceAmount>? ServiceAmounts { get; set; }
    public string? FooterTitle { get; set; }
    public string? FooterDescription { get; set; }
    public string? FooterImage { get; set; }
}


public class Feature
{
    public string? Title { get; set; }
    public IEnumerable<string>? Contents { get; set; }
}

public class ServiceAmount
{
    public string? Name { get; set; }
    public string? Amount { get; set; }
}
