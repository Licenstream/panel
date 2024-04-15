using Domain.Interfaces;
using Infrastructure;
using Domain;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var handler = new MicrosoftLicenseDataHandler(new HttpClient());

            IEnumerable<License> licenses = handler.GetAll();
        }
    }
}
