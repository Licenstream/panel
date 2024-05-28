namespace Domain;

public class Customer
{
    public int Id { get; private set; }
    public string Name { get; }
    public string Adress { get;  }
    public string Country { get;  }
    public IEnumerable<License> Licenses { get; private set; }

    public Customer(int id, string name, string adress, string country)
    {
        Id = id;
        Name = name;
        Adress = adress;
        Country = country;
        Licenses = new List<License>();
    }

    public void SetLicenses(List<License> licenses)
    {
        this.Licenses = licenses;
    }
    
    public void SetId(int newId)
    {
        if (this.Id <= 0)
            this.Id = newId;
        else
            throw new ArgumentException("Id already set");
    }
}
