namespace InfrastructureEF.LicenseModels;

public partial class ServiceStatus
{
    public static IEnumerable<ServiceStatus> ConvertTo(IEnumerable<Domain.ServiceStatus> domainServiceStats)
    {
        var serviceStatList = new List<ServiceStatus>();

        foreach (var item in domainServiceStats)
        {
            serviceStatList.Add(new ServiceStatus()
            {
                ServicePlanId = item.ServicePlanId,
                ServicePlanName = item.ServicePlanName,
                ProvisioningStatus = item.ProvisioningStatus,
                AppliesTo = item.AppliesTo
            });
        }

        return serviceStatList;
    }

    public static IEnumerable<Domain.ServiceStatus> ConvertTo(IEnumerable<ServiceStatus> serviceStats)
    {
        var serviceStatList = new List<Domain.ServiceStatus>();

        foreach (var item in serviceStats)
        {
            serviceStatList.Add(
                new Domain.ServiceStatus(
                    servicePlanId: item.ServicePlanId,
                    servicePlanName: item.ServicePlanName,
                    provisioningStatus: item.ProvisioningStatus,
                    appliesTo: item.AppliesTo
                )
            );
        }

        return serviceStatList;
    }
}