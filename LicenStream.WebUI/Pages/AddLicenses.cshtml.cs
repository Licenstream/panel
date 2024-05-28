using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LicenStream.WebUI.Pages
{
    public class AddLicensesModel : PageModel
    {
        private readonly LicenseService _licenseService;

        [BindProperty]
        public License NewLicense { get; set; }
        
        public AddLicensesModel(LicenseService licenseService)
        {
            _licenseService = licenseService;
        }
        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _licenseService.Save(NewLicense);
            
            return RedirectToPage();
        }
    }
}
