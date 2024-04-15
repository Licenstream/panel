using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LicenStream.WebUI.Pages
{
    public class UsersModel : PageModel
    {
        private readonly Db _dbcontext;
        public List<User> Users { get; set; } = new List<User>();

        [BindProperty]
        public User NewUser { get; set; }


        public UsersModel(Db dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void OnGet()
        {
            Users = _dbcontext.Users.ToList();
        }

        public IActionResult OnPost()
        {
            _dbcontext.Users.Add(NewUser);
            _dbcontext.SaveChanges();
            return RedirectToPage();
        }
    }
}
