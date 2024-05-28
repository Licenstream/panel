using Domain;

namespace LicenStream.WebUI.Pages.Models;

public class CustomerViewModel
{
    public static List<CustomerViewModel> ConvertTo(IEnumerable<Customer> customers)
    {
        var result = new List<CustomerViewModel>();
        
        foreach (var item in customers)
        {
            result.Add(ConvertTo(item));
        }

        return result;
    }
    
    public static CustomerViewModel ConvertTo(Customer customer)
    {
        return new CustomerViewModel()
        {
            Id = customer.Id,
            Name = customer.Name,
            Adress = customer.Adress,
            Country = customer.Country
        };
    }

    public static Customer ConvertTo(CustomerViewModel customerViewModel)
    {
        return new Customer(customerViewModel.Id,
            customerViewModel.Name,
            customerViewModel.Adress,
            customerViewModel.Country);
    }

    public string Country { get; set; }

    public string Adress { get; set; }

    public string Name { get; set; }

    public int Id { get; set; }
}