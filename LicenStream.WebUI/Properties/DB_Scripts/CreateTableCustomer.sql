create table Customer
(
    Id      int(100) auto_increment
        primary key,
    Name    varchar(50) not null,
    Type    varchar(50) not null,
    Company varchar(50) null,
    Email   varchar(50) null,
    Adress  varchar(50) not null,
    Zipcode varchar(50) null,
    City    varchar(50) null,
    State   varchar(50) null,
    Country varchar(50) not null,
    UserId  int(10)     null
);