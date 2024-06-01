using System;
using System.Collections.Generic;

namespace InfrastructureEF.LicenseModels;

public partial class CustomerLicense
{
    public int? CustomerId { get; set; }
    public int? LicenseId { get; set; }
}
