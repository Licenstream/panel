using Domain.Interfaces;
using Domain;
using LicenStream.WebUI.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LicenStream.WebUI.Pages
{
    public class CustomerDetailModel : PageModel
    {
        private readonly IDataHandler<Customer> _handler;
        
        public CustomerViewModel CustomerViewModel { get; set; }
        
        public CustomerDetailModel(IDataHandler<Customer> handler)
        {
            _handler = handler;

            CustomerViewModel = new CustomerViewModel();
        }
        
        public void OnGet(int id)
        {
           var result = _handler.Get(id);

           this.CustomerViewModel = CustomerViewModel.ConvertTo(result);
        }
    }
}
