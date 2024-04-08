using SupplierService;

namespace ConsoleTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            MicrosoftAPI microsoftAPI = new MicrosoftAPI();

            var license = await microsoftAPI.GetLicenseAsync();
        }
    }
}
