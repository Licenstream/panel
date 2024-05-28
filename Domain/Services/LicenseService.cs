using Domain.Interfaces;

namespace Domain;

public class LicenseService
{
    private readonly IDataHandler<License> _licenseDataHandler;

    public LicenseService(IDataHandler<License> licenseDataHandler)
    {
        _licenseDataHandler = licenseDataHandler;
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
        // TODO THIJS (Save to DB)
        
        return true;
    }
}