using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;

namespace LicenStream.WebUI.Pages
{
    public class LicensesModel : PageModel
    {
        private readonly LicenseService _licenseService;

        public IEnumerable<License> Licenses { get; set; }

        public LicensesModel(LicenseService licenseService)
        {
            _licenseService = licenseService;
            Licenses = new List<License>();
        }
        public void OnGet()
        {
            Licenses = _licenseService.GetAll();
        }
    }
}
