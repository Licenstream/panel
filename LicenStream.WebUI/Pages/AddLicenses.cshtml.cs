using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LicenStream.WebUI.Pages
{
    public class AddLicensesModel : PageModel
    {
        private readonly Db _dbcontext;

        [BindProperty]
        public License NewLicense { get; set; }
        
        public AddLicensesModel(Db dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _dbcontext.Licenses.Add(NewLicense);
            _dbcontext.SaveChanges();
            return RedirectToPage();
        }
    }
}
