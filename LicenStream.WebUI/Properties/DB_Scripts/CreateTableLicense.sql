create table License
(
    Id                  int auto_increment
        primary key,
    SkuId               longtext    not null,
    Status              longtext    not null,
    Name                longtext    null,
    TotalLicenses       int         null,
    CreatedDate         datetime(6) not null,
    NextLifeCycleDate   datetime(6) null,
    IsTrail             longtext    null
);

