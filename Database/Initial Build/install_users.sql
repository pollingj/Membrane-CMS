create table MembraneUserType (
	Id UniqueIdentifier primary key clustered not null,
	Type nvarchar(255) not null
)

create table MembraneUser (
	Id UniqueIdentifier primary key clustered not null,
	Type_Id UniqueIdentifier not null,
	Group_Id UniqueIdentifier,
	Username nvarchar(255) not null,
	Password nvarchar(255) not null,
	[Name] nvarchar(255) not null,
	Email nvarchar(255) not null
)

create table UserGroup (
	Id UniqueIdentifier primary key clustered not null,
	Name nvarchar(255) not null
)

alter table MembraneUser add constraint FK_MembraneUser_MembraneUserType foreign key (Type_Id) references MembraneUserType
alter table MembraneUser add constraint FK_MembraneUser_UserGroup foreign key (Group_Id) references UserGroup

insert into MembraneUserType (Id, Type)
values (NewId(), 'Administrator')
insert into MembraneUserType (Id, Type)
values (NewId(), 'User')
insert into MembraneUserType (Id, Type)
values (NewId(), 'Site')

insert into MembraneUser(Id, Type_Id, Username, Password, [Name], Email)
select NewId(), (Select Id From MembraneUserType WHERE Type = 'Administrator'),  'johnpolling', 'B1-09-F3-BB-BC-24-4E-B8-24-41-91-7E-D0-6D-61-8B-90-08-DD-09-B3-BE-FD-1B-5E-07-39-4C-70-6A-8B-B9-80-B1-D7-78-5E-59-76-EC-04-9B-46-DF-5F-13-26-AF-5A-2E-A6-D1-03-FD-07-C9-53-85-FF-AB-0C-AC-BC-86', 'John Polling', 'john@theusualsuspect.com'
insert into MembraneUser(Id, Type_Id, Username, Password, [Name], Email)
select NewId(), (Select Id From MembraneUserType WHERE Type = 'User'),  'andypike', 'B1-09-F3-BB-BC-24-4E-B8-24-41-91-7E-D0-6D-61-8B-90-08-DD-09-B3-BE-FD-1B-5E-07-39-4C-70-6A-8B-B9-80-B1-D7-78-5E-59-76-EC-04-9B-46-DF-5F-13-26-AF-5A-2E-A6-D1-03-FD-07-C9-53-85-FF-AB-0C-AC-BC-86', 'Andy Pike', 'andy@test.com'

insert into UserGroup (Id, Name)
values (NewId(), 'Blogging Group')