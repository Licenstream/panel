using SupplierService;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            MicrosoftAPI microsoftAPI = new MicrosoftAPI();

            var license = microsoftAPI.GetLicense();
        }
    }
}
