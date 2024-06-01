using Domain;

namespace LicenStream.WebUI.Pages.Models;

public class ServiceStatusViewModel
{
    public string ServicePlanId { get; set; }
    public string ServicePlanName { get; set; }
    public string ProvisioningStatus { get; set; }
    public string AppliesTo { get; set; }

    public static IEnumerable<ServiceStatusViewModel> ConvertTo(IEnumerable<ServiceStatus> serviceStatus)
    {
        var result = new List<ServiceStatusViewModel>();

        foreach (var item in serviceStatus)
        {
            result.Add(ConvertTo(item));
        }

        return result;
    }

    public static IEnumerable<ServiceStatus> ConvertTo(IEnumerable<ServiceStatusViewModel> serviceStatusViewModels)
    {
        var result = new List<ServiceStatus>();

        foreach (var item in serviceStatusViewModels)
        {
            result.Add(ConvertTo(item));
        }

        return result;
    }

    public static ServiceStatusViewModel ConvertTo(ServiceStatus serviceStatus)
    {
        return new ServiceStatusViewModel()
        {
           ServicePlanId = serviceStatus.ServicePlanId,
           ServicePlanName = serviceStatus.ServicePlanName,
           ProvisioningStatus = serviceStatus.ProvisioningStatus,
           AppliesTo = serviceStatus.AppliesTo
        };
    }

    public static ServiceStatus ConvertTo(ServiceStatusViewModel serviceStatusViewModel)
    {
        return new ServiceStatus(
            serviceStatusViewModel.ServicePlanId,
            serviceStatusViewModel.ServicePlanName,
            serviceStatusViewModel.ProvisioningStatus,
            serviceStatusViewModel.AppliesTo
        );
    }
}