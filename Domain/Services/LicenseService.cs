using Domain.Interfaces;

namespace Domain;

public class LicenseService
{
    private readonly IDataHandler<License> _licenseDataHandler;
    private readonly IDataBulkHandler<License> _dataBulkHandler;

    public LicenseService(IDataHandler<License> licenseDataHandler, IDataBulkHandler<License> dataBulkHandler)
    {
        _licenseDataHandler = licenseDataHandler;
        _dataBulkHandler = dataBulkHandler;
    }

    public License GetById(int id)
    {
        return _licenseDataHandler.Get(id);
    }

    public IEnumerable<License> GetAll()
    {
        return _licenseDataHandler.GetAll();
    }

    public License Save(License license)
    {
        var newId = _licenseDataHandler.Insert(license);

        license.SetId(newId);

        return license;
    }

    public bool SaveToCustomer(IEnumerable<License> licenses, int customerId)
    {
        try
        {
            _dataBulkHandler.InsertBulk(licenses, customerId);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
}