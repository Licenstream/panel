using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LicenStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicrosoftLicenseController : ControllerBase
    {
        // GET: api/<MicrosoftLicenseController>
        [HttpGet]
        public IEnumerable<string> GetLicenses()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MicrosoftLicenseController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
