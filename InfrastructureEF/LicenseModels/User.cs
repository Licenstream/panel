using System;
using System.Collections.Generic;

namespace InfrastructureEF.LicenseModels;

public partial class User
{
    public int Id { get; set; }
    public int Name { get; set; }

    public int? Username { get; set; }
    public int Email { get; set; }

    public int? Password { get; set; }

    public int? Active { get; set; }
}
