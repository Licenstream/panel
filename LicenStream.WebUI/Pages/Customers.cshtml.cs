using Domain.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LicenStream.WebUI.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly Db dbcontext;
        private readonly IDataHandler<Customer> _handler;

        public List<Customer> Licenses { get; set; } = new();

        public CustomersModel(IDataHandler<Customer> handler)
        {
            _handler = handler;
        }
        public void OnGet()
        {
        }
    }
}
