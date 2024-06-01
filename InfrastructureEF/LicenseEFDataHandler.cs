using System.Text;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Customer = InfrastructureEF.LicenseModels.Customer;
using ServiceStatus = InfrastructureEF.LicenseModels.ServiceStatus;

namespace InfrastructureEF;

public class LicenseEFDataHandler : IDataHandler<Domain.License>, IDataBulkHandler<Domain.License>
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
            var license = context.License
                .FirstOrDefault(l => l.Id == id);

            return  LicenseModels.License.ConvertTo(license);
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
                var newObject = LicenseModels.License.ConvertTo(license);
                
                licenseList.Add(newObject);
            }
        }

        return licenseList;
    }

    public int Insert(License dataType)
    {
        using (var context = new LicenseContext(_connectionString))
        {
            var newObject = LicenseModels.License.ConvertTo(dataType);

            context.License.Add(newObject);

            // Saves changes
            context.SaveChanges();

            return newObject.Id;
        }
    }

    public void InsertBulk(IEnumerable<License> dataTypes, int customerId)
    {
        using (var context = new LicenseContext(_connectionString))
        {
            var newObjects = new List<LicenseModels.License>();
           
            foreach (var item in dataTypes)
            {
                var newObject = LicenseModels.License.ConvertTo(item);
                
                newObjects.Add(newObject);
            }

            context.License.AddRange(newObjects);

            // Saves changes
            context.SaveChanges();
        }
    }
}