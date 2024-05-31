namespace Domain;

public class Customer
{
    public int Id { get; private set; }
    public string Name { get; }
    public string? Type { get; }
    public string? Company { get; }
    public string? Email { get; }
    public string Adress { get; }
    public string? Zipcode { get; }
    public string? City { get; }
    public string? State { get; }
    public string Country { get; }
    public int? Userid { get; }
    public IEnumerable<License> Licenses { get; private set; }

    public Customer(int id, string name, string type, string company, string email,
        string adress, string zipcode, string city, string state, string country, int? userid)
    {
        Id = id;
        Name = name;
        Type = type;
        Company = company;
        Email = email;
        Adress = adress;
        Zipcode = zipcode;
        City = city;
        State = state;
        Country = country;
        Userid = userid;
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