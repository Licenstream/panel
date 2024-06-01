using System;
using System.Collections.Generic;

namespace InfrastructureEF.LicenseModels;

public partial class ServiceStatus
{
    public int Id { get; set; }

    public string ServicePlanId { get; set; } = null!;

    public string ServicePlanName { get; set; } = null!;

    public string? ProvisioningStatus { get; set; }

    public string? AppliesTo { get; set; }
}
