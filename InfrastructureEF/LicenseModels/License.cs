using System;
using System.Collections.Generic;

namespace InfrastructureEF.LicenseModels;

public partial class License
{
    public int Id { get; set; }

    public string SkuId { get; set; } = null!;

    public string? Name { get; set; }

    public string Status { get; set; } = null!;

    public int? TotalLicenses { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? NextLifeCycleDate { get; set; }

    public string? IsTrail { get; set; }
}
