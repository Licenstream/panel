using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class License
{
    public int Id { get; private set; }
    public string SkuPartNumber { get; }
    public string Status { get; }
    public int TotalLicenses { get; }
    public DateTime CreatedDate { get; }
    public DateTime NextLifeCycleDate { get; }
    public bool IsTrail { get; }

    public License(int id, string skuPartNumber, string status, int totalLicenses, DateTime createdDate, DateTime nextLifeCycleDate, bool isTrail)
    {
        Id = id;
        SkuPartNumber = skuPartNumber;
        Status = status;
        TotalLicenses = totalLicenses;
        CreatedDate = createdDate;
        NextLifeCycleDate = nextLifeCycleDate;
        IsTrail = isTrail;
    }

    public void SetId(int newId)
    {
        if (this.Id <= 0)
            this.Id = newId;
        else
            throw new ArgumentException("Id already set");
    }

    public override string ToString()
    {
        return $"{Id} {SkuPartNumber} {Status} {TotalLicenses} {CreatedDate} {NextLifeCycleDate} {IsTrail}";
    }
}