namespace Domain;

public class Customer : User
{
    public List<License>? Licenses { get; set; }
}
