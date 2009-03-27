create table MembraneUserType (
	Id UniqueIdentifier primary key clustered not null,
	Name nvarchar(255)
)

create table MembraneUser (
	Id UniqueIdentifier primary key clustered not null,
	Type_Id UniqueIdentifier,
	Username nvarchar(255),
	Password nvarchar(255)
)

alter table MembraneUser add constraint FK_MembraneUser_MembraneUserType foreign key (Type_Id) references MembraneUserType

insert into MembraneUserType (Id, Name)
values (NewId(), 'Administrator')
insert into MembraneUserType (Id, Name)
values (NewId(), 'User')
insert into MembraneUserType (Id, Name)
values (NewId(), 'Site')

insert into MembraneUser(Id, Type_Id, Username, Password)
select NewId(), (Select Id From MembraneUserType WHERE Name = 'Administrator'),  'johnpolling', 'password'
insert into MembraneUser(Id, Type_Id, Username, Password)
select NewId(), (Select Id From MembraneUserType WHERE Name = 'User'),  'andypike', 'password'

	
