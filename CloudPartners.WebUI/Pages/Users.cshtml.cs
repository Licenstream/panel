using CloudPartners.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CloudPartners.WebUI.Pages
{
    public class UsersModel : PageModel
    {
        private readonly Db dbcontext;


        [BindProperty]
        public User NewUser { get; set; }


        public UsersModel(Db context)
        {
            dbcontext = context;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            return RedirectToPage();
        }
    }
}
