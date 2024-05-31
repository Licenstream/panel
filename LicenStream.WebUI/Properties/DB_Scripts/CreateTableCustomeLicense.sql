create table CustomerLicense
(
    CustomerId int not null,
    LicenseId  int not null,
    constraint CustomerLicense_Customer_Id_fk
        foreign key (CustomerId) references Customer (Id),
    constraint CustomerLicense_License_Id_fk
        foreign key (LicenseId) references License (Id)
);