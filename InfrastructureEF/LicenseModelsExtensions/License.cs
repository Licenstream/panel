using System.Globalization;

namespace InfrastructureEF.LicenseModels;

public partial class License
{
    public List<ServiceStatus>? ServiceStats { get; set; } = new ();
    
    public static License ConvertTo(Domain.License domainLicense)
    {
        var newObject = new License()
        {
            SkuId = domainLicense.SkuId,
            Status = domainLicense.Status,
            Name = domainLicense.Name,
            TotalLicenses = Convert.ToInt32(domainLicense.TotalLicenses),
            CreatedDate = domainLicense.CreatedDate,
            NextLifeCycleDate = domainLicense.NextLifeCycleDate,
            IsTrail = domainLicense.IsTrail.ToString(),
        };
        
        return newObject;
    }

    public static Domain.License ConvertTo(License efLicense)
    {
        var isTrail = (efLicense.IsTrail.ToLower() == bool.TrueString ? true : false);
        var newObject = new Domain.License(
            id: efLicense.Id,
            skuId: efLicense.SkuId,
            status: efLicense.Status,
            name: efLicense.Name,
            totalLicenses: Convert.ToInt32(efLicense.TotalLicenses),
            createdDate: efLicense.CreatedDate,
            nextLifeCycleDate: efLicense.NextLifeCycleDate,
            isTrail: isTrail
        );

        return newObject;
    }
}