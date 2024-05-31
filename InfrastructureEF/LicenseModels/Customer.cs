using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace InfrastructureEF.LicenseModels;

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Type { get; set; }
    public string? Company { get; set; }
    public string? Email { get; set; }
    public string Adress { get; set;}
    public string? Zipcode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string Country { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public  ICollection<License> Licenses { get; set; }
}