using System;
using System.Collections.Generic;

namespace InfrastructureEF.LicenseModels;

public partial class LicenseServiceStatus
{
    public int? LicenseId { get; set; }
    public int? ServiceStatusId { get; set; }
}
