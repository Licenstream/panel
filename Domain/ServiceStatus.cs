namespace Domain;

public class ServiceStatus
{
    public string ServicePlanId { get; }
    public string ServicePlanName { get; }
    public string ProvisioningStatus { get; }
    public string AppliesTo { get; }

    public ServiceStatus(string servicePlanId, string servicePlanName, string provisioningStatus, 
        string appliesTo)
    {
        ServicePlanId = servicePlanId;
        ServicePlanName = servicePlanName;
        ProvisioningStatus = provisioningStatus;
        AppliesTo = appliesTo;
    }
}