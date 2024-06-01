namespace Domain;

public class ServiceStatus
{
    public int Id { get; private set; }
    public string ServicePlanId { get; }
    public string ServicePlanName { get; }
    public string ProvisioningStatus { get; }
    public string AppliesTo { get; }

    public ServiceStatus(string servicePlanId, string servicePlanName, string provisioningStatus, 
        string appliesTo)
    {
        Id = -1;
        ServicePlanId = servicePlanId;
        ServicePlanName = servicePlanName;
        ProvisioningStatus = provisioningStatus;
        AppliesTo = appliesTo;
    }

    public void SetId(int newId)
    {
        this.Id = newId;
    }
}