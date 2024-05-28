using Domain.Interfaces;

namespace Domain;

public class CustomerService
{
    private readonly IDataHandler<Customer> _customerDataHandler;

    public CustomerService(IDataHandler<Customer> customerDataHandler)
    {
        _customerDataHandler = customerDataHandler;
    }

    public Customer GetById(int id)
    {
        return _customerDataHandler.Get(id);
    }

    public IEnumerable<Customer> GetAll()
    {
        return _customerDataHandler.GetAll();
    }

    public Customer Save(Customer customer)
    {
        var newId = _customerDataHandler.Insert(customer);
        
        customer.SetId(newId);

        return customer;
    }
}