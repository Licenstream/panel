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
            Type = customer.Type,
            Company = customer.Company,
            Email = customer.Email,
            Adress = customer.Adress,
            Zipcode = customer.Zipcode,
            City = customer.City,
            State = customer.State,
            Country = customer.Country,
            UserId = customer.Userid
        };
    }

    public static Customer ConvertTo(CustomerViewModel customerViewModel)
    {
        return new Customer(customerViewModel.Id,
            customerViewModel.Name,
            customerViewModel.Type,
            customerViewModel.Company,
            customerViewModel.Email,
            customerViewModel.Adress,
            customerViewModel.Zipcode,
            customerViewModel.City,
            customerViewModel.State,
            customerViewModel.Country,
            customerViewModel.UserId);
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Company { get; set; }
    public string Email { get; set; }
    public string Adress { get; set; }
    public string Zipcode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public int? UserId { get; set; }
}