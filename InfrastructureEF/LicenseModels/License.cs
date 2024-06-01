namespace InfrastructureEF.LicenseModels;

public class License
{
    public int Id { get; set; }
    public string SkuId { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public int TotalLicenses { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime NextLifeCycleDate { get; set; }
    public bool IsTrail { get; set; }
    public ICollection<ServiceStatus> ServiceStats { get; set; }

    public static License ConvertTo(Domain.License domainLicense)
    {
        License newObject = new License()
        {
            SkuId = domainLicense.SkuId,
            Status = domainLicense.Status,
            Name = domainLicense.Name,
            TotalLicenses = Convert.ToInt32(domainLicense.TotalLicenses),
            CreatedDate = domainLicense.CreatedDate,
            NextLifeCycleDate = domainLicense.NextLifeCycleDate,
            IsTrail = domainLicense.IsTrail,
        };

        var serviceStatList = new List<ServiceStatus>();

        foreach (var item in domainLicense.ServiceStats)
        {
            serviceStatList.Add(new ServiceStatus()
            {
                ServicePlanId = item.ServicePlanId,
                ServicePlanName = item.ServicePlanName,
                ProvisioningStatus = item.ProvisioningStatus,
                AppliesTo = item.AppliesTo
            });
        }

        newObject.ServiceStats = serviceStatList;

        return newObject;
    }

    public static Domain.License ConvertTo(License efLicense)
    {
        Domain.License newObject = new Domain.License(
            id: efLicense.Id,
            skuId: efLicense.SkuId,
            status: efLicense.Status,
            name: efLicense.Name,
            totalLicenses: Convert.ToInt32(efLicense.TotalLicenses),
            createdDate: efLicense.CreatedDate,
            nextLifeCycleDate: efLicense.NextLifeCycleDate,
            isTrail: efLicense.IsTrail
        );

        foreach (var item in efLicense.ServiceStats)
        {
            newObject.ServiceStats.Add(
                new Domain.ServiceStatus(
                    servicePlanId: item.ServicePlanId,
                    servicePlanName: item.ServicePlanName,
                    provisioningStatus: item.ProvisioningStatus,
                    appliesTo: item.AppliesTo
                )
            );
        }

        return newObject;
    }
}