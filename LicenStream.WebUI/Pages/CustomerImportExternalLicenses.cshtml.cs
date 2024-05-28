using System.Collections;
using Domain;
using Infrastructure;
using InfrastructureEF;
using InfrastructureEF.LicenseModels;
using LicenStream.WebUI.Pages.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using License = Domain.License;

namespace LicenStream.WebUI.Pages;

public class CustomerImportExternalLicenses : PageModel
{
    private readonly IMemoryCache _cache;
    public int CustomerId { get; set; }
    public IEnumerable<LicenseViewModel> Licenses { get; set; }

    public CustomerImportExternalLicenses(IMemoryCache cache)
    {
        _cache = cache;
        Licenses = new List<LicenseViewModel>();
    }

    public void OnGet(int customerId)
    {
        var cacheKey = $"licenseList_{customerId}";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<LicenseViewModel> customerlicenses))
        {
            this.CustomerId = customerId;
            var handler = new MicrosoftLicenseApiHandler();
            var result = handler.GetLicenses();
            customerlicenses = LicenseViewModel.ConvertTo(result);
            _cache.Set(cacheKey, customerlicenses, TimeSpan.FromMinutes(10));
        }

        Licenses = _cache.Get<IEnumerable<LicenseViewModel>>(cacheKey);
    }

    public void SaveImportedExternalLicenses(int customerId)
    {
        var cacheKey = $"licenseList_{customerId}";
        var licenseService = new LicenseService(new LicenseEFDataHandler());

        if (_cache.TryGetValue(cacheKey, out IEnumerable<LicenseViewModel> customerlicenses))
        {
            var domainLicenses = LicenseViewModel.ConvertTo(customerlicenses);
            licenseService.SaveToCustomer(domainLicenses, customerId);
        }
    }
}