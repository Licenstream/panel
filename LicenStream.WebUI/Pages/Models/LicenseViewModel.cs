using Domain;

namespace LicenStream.WebUI.Pages.Models;

public class LicenseViewModel
{
    public int Id { get; set; } 
    public string SkuPartNumber { get; set; }
    public bool IsTrail { get; set; }
    public DateTime NextLifeCycleDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public int TotalLicenses { get; set; }
    public string Status { get; set; }

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
            SkuPartNumber = license.SkuPartNumber,
            Status = license.Status,
            TotalLicenses = license.TotalLicenses,
            CreatedDate = license.CreatedDate,
            NextLifeCycleDate = license.NextLifeCycleDate,
            IsTrail = license.IsTrail
        };
    }

    public static License ConvertTo(LicenseViewModel customerViewModel)
    {
        return new License(customerViewModel.Id,
            customerViewModel.SkuPartNumber,
            customerViewModel.Status,
            customerViewModel.TotalLicenses,
            customerViewModel.CreatedDate,
            customerViewModel.NextLifeCycleDate,
            customerViewModel.IsTrail);
    }


}