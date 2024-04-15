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
        private readonly Db dbcontext;
        private readonly IDataHandler<License> _handler;

        public List<License> Licenses { get; set; } = new();

        public LicensesModel(IDataHandler<License> licenseHandler, Db context)
        {
            dbcontext = context;
            _handler = licenseHandler;
        }
        public void OnGet()
        {
            Licenses = dbcontext.Licenses.ToList();
            foreach (var license in _handler.GetAll()) 
            {
                Licenses.Add(license);
            }
        }
    }
}
