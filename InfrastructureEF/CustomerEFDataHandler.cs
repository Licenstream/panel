using Domain;
using Domain.Interfaces;

namespace InfrastructureEF;

public class CustomerEFDataHandler: IDataHandler<Domain.Customer>
{
    private readonly string _connectionString;

    public CustomerEFDataHandler(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public Customer Get(int id)
    {
        using (var context = new LicenseContext(_connectionString))
        {
            var result = context.Customer
                .FirstOrDefault(l => l.Id == id);

            return new Customer(result.Id, result.Name, result.Adress, result.Country);
        }
    }

    public IEnumerable<Customer> GetAll()
    {
        var customerList = new List<Domain.Customer>();

        using (var context = new LicenseContext(_connectionString))
        {
            var result = context.Customer;
            foreach (var item in result)
            {
                customerList.Add(new Customer(item.Id, item.Name, item.Adress, item.Country));
            }
        }

        return customerList;
    }

    public int Insert(Customer dataType)
    {
        using (var context = new LicenseContext(_connectionString)){
            
            var newObject = new LicenseModels.Customer
            {
                Name = dataType.Name,
                Adress = dataType.Adress,
                Country = dataType.Country,
                Type = null,
                Company = null,
                Email = null,
                Zipcode = null,
                City = null,
                State = null,
                UserId = null,
                User = null,
                Licenses = null
            };
            
            var efLicenses = new List<LicenseModels.License>();
            foreach (var dataTypeLicense in dataType.Licenses)
            {
                efLicenses.Add(new LicenseModels.License
                {
                    Id = dataTypeLicense.Id,
                    Name = dataTypeLicense.SkuPartNumber,
                    SkuPartNumber = dataTypeLicense.SkuPartNumber,
                    Status = dataTypeLicense.Status,
                    Type = null,
                    Brand = null, // dataTypeLicense.Brand,
                    Usage = dataTypeLicense.TotalLicenses,
                    Count = dataTypeLicense.TotalLicenses,
                    ExpiredDate = dataTypeLicense.NextLifeCycleDate,
                    CustomerId = newObject.Id,
                    Customer = newObject,
                });
            }
            
            context.Customer.Add(newObject);

            // Saves changes
            context.SaveChanges();

            return newObject.Id;
        }
    }
}