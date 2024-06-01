create table ServiceStatus
(
    Id                  int auto_increment
        primary key,
    ServicePlanId       longtext    not null,
    ServicePlanName     longtext    not null,
    ProvisioningStatus  longtext    null,
    AppliesTo           longtext    null
);