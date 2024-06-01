using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class License
{
    public int Id { get; private set; }
    public string SkuId { get; }
    public string Status { get; }
    public string Name { get; }
    public int TotalLicenses { get; }
    public DateTime CreatedDate { get; }
    public DateTime? NextLifeCycleDate { get; }
    public bool IsTrail { get; }
    public List<ServiceStatus> ServiceStats { get; }

    public License(int id, string skuId, string status, string name, int totalLicenses, DateTime createdDate, 
        DateTime? nextLifeCycleDate, bool isTrail)
    {
        Id = id;
        SkuId = skuId;
        Status = status;
        Name = name;
        TotalLicenses = totalLicenses;
        CreatedDate = createdDate;
        NextLifeCycleDate = nextLifeCycleDate;
        IsTrail = isTrail;
        ServiceStats = new List<ServiceStatus>();
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
        return $"{Id} {SkuId} {Status} {TotalLicenses} {CreatedDate} {NextLifeCycleDate} {IsTrail}";
    }
}

