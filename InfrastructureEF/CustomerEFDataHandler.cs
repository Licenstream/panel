using Domain;
using Domain.Interfaces;
using License = InfrastructureEF.LicenseModels.License;
using User = InfrastructureEF.LicenseModels.User;

namespace InfrastructureEF;

public class CustomerEFDataHandler : IDataHandler<Domain.Customer>
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

            return new Customer(result.Id, result.Name, result.Type, result.Company, result.Email,
                result.Adress, result.Zipcode, result.City, result.State, result.Country, result.UserId);
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
                customerList.Add(
                    new Customer(item.Id, item.Name, item.Type, item.Company, item.Email,
                        item.Adress, item.Zipcode, item.City, item.State, item.Country,
                        item.UserId)
                );
            }
        }

        return customerList;
    }

    public int Insert(Customer dataType)
    {
        using (var context = new LicenseContext(_connectionString))
        {
            var newObject = new LicenseModels.Customer
            {
                Name = dataType.Name,
                Type = dataType.Type,
                Company = dataType.Country,
                Email = dataType.Email,
                Adress = dataType.Adress,
                Zipcode = dataType.Zipcode,
                City = dataType.City,
                State = dataType.State,
                Country = dataType.Country,
                UserId = dataType.Userid,
                User = new User(),
                Licenses = new List<License>()
            };

            foreach (var license in dataType.Licenses)
            {
                newObject.Licenses.Add(LicenseModels.License.ConvertTo(license));
            }
            
            context.Customer.Add(newObject);

            // Saves changes
            context.SaveChanges();

            return newObject.Id;
        }
    }
}