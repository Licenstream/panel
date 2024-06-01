using System;
using System.Collections.Generic;

namespace InfrastructureEF.LicenseModels;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Company { get; set; }

    public string? Email { get; set; }

    public string Adress { get; set; } = null!;

    public string? Zipcode { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string Country { get; set; } = null!;

    public int? UserId { get; set; }
}
