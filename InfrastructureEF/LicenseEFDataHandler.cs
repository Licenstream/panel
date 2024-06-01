using System.Diagnostics;
using System.Text;
using Domain.Interfaces;
using InfrastructureEF.Context;
using InfrastructureEF.LicenseModels;
using Microsoft.EntityFrameworkCore;
using Customer = InfrastructureEF.LicenseModels.Customer;
using License = Domain.License;
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

            return LicenseModels.License.ConvertTo(license);
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
            try
            {
                var newEfLicenseList = SaveLicenses(context, dataTypes);

                var newLicenseServiceStatusList = SaveLicenseServiceStatus(context, newEfLicenseList);

                SaveCustomerLicenses(context, customerId, newLicenseServiceStatusList);
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e.Message);
#endif
                throw;
            }
        }
    }

    private void SaveCustomerLicenses(LicenseContext context, int customerId,
        List<LicenseServiceStatus> newLicenseServiceStatusList)
    {
        foreach (var licenceId in newLicenseServiceStatusList.Select(x => x.LicenseId).Distinct())
        {
            var customerLicense = new CustomerLicense() { CustomerId = customerId, LicenseId = licenceId };
            context.CustomerLicense.Add(customerLicense);
        }

        context.SaveChanges();
    }

    private List<LicenseServiceStatus> SaveLicenseServiceStatus(LicenseContext context,
        List<LicenseModels.License> efLicenses)
    {
        var newLicenseServiceStatus = new List<LicenseModels.LicenseServiceStatus>();
        foreach (var efLicense in efLicenses)
        {
            foreach (var serviceStatus in efLicense.ServiceStats)
            {
                newLicenseServiceStatus.Add(
                    new LicenseServiceStatus() { LicenseId = efLicense.Id, ServiceStatusId = serviceStatus.Id });
            }
        }

        context.LicenseServiceStatus.AddRange(newLicenseServiceStatus);
        context.SaveChanges();

        return newLicenseServiceStatus;
    }

    private List<LicenseModels.License> SaveLicenses(LicenseContext context, IEnumerable<License> licenses)
    {
        var efLicenses = new List<LicenseModels.License>();
        foreach (var item in licenses)
        {
            var newLicense = LicenseModels.License.ConvertTo(item);
            var newServiceStats = LicenseModels.ServiceStatus.ConvertTo(item.ServiceStats);
            newLicense.ServiceStats.AddRange(newServiceStats);
            efLicenses.Add(newLicense);
        }

        context.License.AddRange(efLicenses);
        context.SaveChanges();

        foreach (var efLicence in efLicenses)
        {
            context.ServiceStatus.AddRange(efLicence.ServiceStats);
        }

        context.SaveChanges();

        return efLicenses;
    }
}