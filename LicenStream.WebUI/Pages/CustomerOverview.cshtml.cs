using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain;
using LicenStream.WebUI.Pages.Models;

namespace LicenStream.WebUI.Pages
{
    public class CustomerOverviewModel : PageModel
    {
        private readonly CustomerService _customerService;
        public IEnumerable<CustomerViewModel> Customers { get;set; } 
     
        public CustomerOverviewModel(CustomerService customerService)
        {
            _customerService = customerService;

            Customers = new List<CustomerViewModel>();
        }
        
        public void OnGet()
        {
            var result = _customerService.GetAll();

            Customers = CustomerViewModel.ConvertTo(result);
        }
    }
}
