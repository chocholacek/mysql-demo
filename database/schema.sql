CREATE SCHEMA marko;

USE marko;

create table address (
	Id int not null auto_increment,
    Street varchar (50) not null,
    StreetNumer int,
    City varchar(50) not null,
    ZipCode varchar(5) not null,
    primary key (Id)
);

create table person (
	Id int not null auto_increment,
    FirstName varchar(50),
    LastName varchar(50) not null,
    Age int not null,
    AddressId int not null,
    
    primary key (Id),
    foreign key (AddressId) references address(Id)
);

create table ability (
	Id int not null auto_increment,
    Name varchar(50) not null,
    
    primary key (Id)
);

create table powers (
	personId int not null,
    abilityId int not null,
    
    primary key (personId, abilityId),
    foreign key (personId) references person(Id),
    foreign key (abilityId) references ability(Id)
);