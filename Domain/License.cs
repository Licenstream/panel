namespace Domain;

public class License
{
    public string AccountName { get; set; }

    public string AccountId { get; set; }

    public int Id { get; set; }

    public string CompanyName {  get; set; }

    public string Description { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public Supplier Supplier { get; set; }

    public License()
    {
        
    }
}
