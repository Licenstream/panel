create table LicenseServiceStatus
(
    LicenseId  int not null,
    ServiceStatusId int not null,
    constraint LicenseServiceStatus_License_Id_fk
        foreign key (LicenseId) references License (Id),
    constraint LicenseServiceStatus_ServiceStatus_Id_fk
        foreign key (ServiceStatusId) references ServiceStatus (Id)
);