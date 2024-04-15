using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Domain.Interfaces;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace LicenStream.WebUI.Pages
{
    public class LicenseDetailsModel : PageModel
    {
        public Domain.License License;
        public IDataHandler<Domain.License> _handler;

        public LicenseDetailsModel(IDataHandler<Domain.License> handler)
        {
            _handler = handler;
        }
        public void OnGet(string objectId)
        {
            License = _handler.Get(objectId);
        }
    }
}
