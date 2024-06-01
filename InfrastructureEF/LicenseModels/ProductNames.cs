using System;
using System.Collections.Generic;

namespace InfrastructureEF.LicenseModels;

public partial class ProductNames
{
    public string? ProductDisplayName { get; set; }

    public string? StringId { get; set; }

    public string? Guid { get; set; }

    public string? ServicePlanName { get; set; }

    public string? ServicePlanId { get; set; }

    public string? ServicePlansIncludedFriendlyNames { get; set; }
}
