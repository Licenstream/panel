namespace InfrastructureEF.LicenseModels;

public class License
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SkuPartNumber { get; set; }
    public string Status { get; set; }
    public int? Type { get; set; }
    public string? Brand { get; set; }
    public int? Usage { get; set; }
    public int? Count { get; set; }
    public DateTime? ExpiredDate { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
}