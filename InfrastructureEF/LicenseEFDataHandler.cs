using System.Text;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Customer = InfrastructureEF.LicenseModels.Customer;

namespace InfrastructureEF;

public class LicenseEFDataHandler : IDataHandler<Domain.License>
{
    private readonly string _connectionString;

    public LicenseEFDataHandler(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public License Get(int id)
    {
        using (var context = new LicenseContext(_connectionString))
        {
            var result = context.License
                .FirstOrDefault(l => l.Id == id);

            return new License(result.Id, result.SkuPartNumber, result.Status, 
                0, DateTime.Now, DateTime.Now, false);
        }
    }

    public IEnumerable<Domain.License> GetAll()
    {
        var licenseList = new List<Domain.License>();

        using (var context = new LicenseContext(_connectionString))
        {
            var result = context.License;
            foreach (var license in result)
            {
                licenseList.Add(new License(license.Id, license.SkuPartNumber, license.Status, 
                    0, DateTime.Now, DateTime.Now, false));
            }
        }

        return licenseList;
    }

    public int Insert(License dataType)
    {
        using (var context = new LicenseContext(_connectionString))
        {
            var newObject = new LicenseModels.License()
            {
                SkuPartNumber = dataType.SkuPartNumber,
                Status = dataType.Status,
                ExpiredDate = dataType.NextLifeCycleDate,
                Count = Convert.ToInt32(dataType.TotalLicenses),
                Name = dataType.SkuPartNumber,
                Brand = dataType.SkuPartNumber,
                Type = 1,
                Customer = null
            };

            context.License.Add(newObject);

            // Saves changes
            context.SaveChanges();

            return newObject.Id;
        }
    }

    public void InsertLicenseData()
    {
        using (var context = new LicenseContext(_connectionString))
        {
            // Creates the database if not exists
            // context.Database.EnsureCreated(); UIT !!!

            // Adds a customer
            var customer = new LicenseModels.Customer()
            {
                Name = "Customer A",
                Adress = "Rachelsmolen",
                Country = "NL",
                City = "Eindhoven"
            };
            context.Customer.Add(customer);

            // Adds some licenses
            context.License.Add(new LicenseModels.License()
            {
                Name = "MS Office License",
                Brand = "Microsoft",
                Count = 145,
                ExpiredDate = DateTime.Now.AddDays(3),
                Type = 1,
                Status = "InUse",
                SkuPartNumber = "MW4433FF#",
                Customer = customer
            });
            context.License.Add(new LicenseModels.License()
            {
                Name = "MS Teams License",
                Brand = "Microsoft",
                Count = 256,
                ExpiredDate = DateTime.Now.AddDays(16),
                Type = 1,
                Status = "ReadyToUse",
                SkuPartNumber = "EA5633DD#",
                Customer = customer
            });

            // Saves changes
            context.SaveChanges();
        }
    }
}