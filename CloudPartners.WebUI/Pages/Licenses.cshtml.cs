using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;

namespace CloudPartners.WebUI.Pages
{
    public class LicensesModel : PageModel
    {
        private readonly Db dbcontext;
        
        public List<License> Licenses { get; set; } = new List<License>();

        public LicensesModel(Db context)
        {
            dbcontext = context;
        }
        public void OnGet()
        {
            Licenses = dbcontext.Licenses.ToList();
        }
    }
}
