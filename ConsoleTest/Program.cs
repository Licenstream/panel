using Infrastructure;
using Domain;
using Domain.Interfaces;
using InfrastructureEF;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunMicrosoftLicenseApiCall();

            // RunApiCallRob();

            // RunEFCall();
        }

        private static void RunMicrosoftLicenseApiCall()
        {
            var handler = new MicrosoftLicenseApiHandler();

            try
            {
                var licenses = handler.GetLicenses();

                Console.WriteLine(string.Join(",", licenses));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void RunApiCallRob()
        {
            var handler = new MicrosoftLicenseDataHandler(new HttpClient());

            PrintResult(handler);
        }

        private static void RunEFCall()
        {
            var handler = new LicenseEFDataHandler();

            handler.InsertLicenseData();

            PrintResult(handler);
        }

        private static void PrintResult(IDataHandler<License> handler)
        {
            IEnumerable<License> licenses = handler.GetAll();

            foreach (var item in licenses)
            {
                Console.WriteLine($"ID:{item.Id}, SkuPartNumber:{item.SkuPartNumber}, Status:{item.Status}");
            }

            Console.WriteLine("Press any key...");
        }
    }
}