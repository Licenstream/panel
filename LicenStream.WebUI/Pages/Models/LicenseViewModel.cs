using Domain;
using ServiceStatus = InfrastructureEF.LicenseModels.ServiceStatus;

namespace LicenStream.WebUI.Pages.Models;

public class LicenseViewModel
{
    public int Id { get; set; }
    public string SkuId { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public int TotalLicenses { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime NextLifeCycleDate { get; set; }
    public bool IsTrail { get; set; }
    public IEnumerable<ServiceStatusViewModel> ServiceStats { get; set; }

    public static IEnumerable<LicenseViewModel> ConvertTo(IEnumerable<License> licenses)
    {
        var result = new List<LicenseViewModel>();

        foreach (var item in licenses)
        {
            result.Add(ConvertTo(item));
        }

        return result;
    }

    public static IEnumerable<License> ConvertTo(IEnumerable<LicenseViewModel> licenses)
    {
        var result = new List<License>();

        foreach (var item in licenses)
        {
            result.Add(ConvertTo(item));
        }

        return result;
    }

    public static LicenseViewModel ConvertTo(License license)
    {
        return new LicenseViewModel()
        {
            Id = license.Id,
            SkuId = license.SkuId,
            Status = license.Status,
            Name = license.Name,
            TotalLicenses = license.TotalLicenses,
            CreatedDate = license.CreatedDate,
            NextLifeCycleDate = license.NextLifeCycleDate,
            IsTrail = license.IsTrail,
            ServiceStats = ServiceStatusViewModel.ConvertTo(license.ServiceStats)
        };
    }

    public static License ConvertTo(LicenseViewModel customerViewModel)
    {
        var license = new License(customerViewModel.Id,
            customerViewModel.SkuId,
            customerViewModel.Status,
            customerViewModel.Name,
            customerViewModel.TotalLicenses,
            customerViewModel.CreatedDate,
            customerViewModel.NextLifeCycleDate,
            customerViewModel.IsTrail);
        
        license.ServiceStats.AddRange(ServiceStatusViewModel.ConvertTo(customerViewModel.ServiceStats));

        return license;
    }
}