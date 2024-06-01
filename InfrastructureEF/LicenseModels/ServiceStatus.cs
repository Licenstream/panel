namespace InfrastructureEF.LicenseModels;

public class ServiceStatus
{
    public int Id { get; set; }
    public string ServicePlanId { get; set; }
    public string ServicePlanName { get; set; }
    public string ProvisioningStatus { get; set; }
    public string AppliesTo { get; set; }
}