using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class License
{
    public string? AccountName { get; set; }

    public string? AccountId { get; set; }

    public string Id { get; set; }

    public string SkuPartNumber { get; set; }

    public int ConsumedUnits {  get; set; }

    [NotMapped]
    public PrepaidUnits? PrepaidUnits { get; set; }

    [NotMapped]
    public List<ServicePlan> ServicePlans { get; set; }

    public Supplier? Supplier { get; set; }

    public License()
    {
        
    }
}
