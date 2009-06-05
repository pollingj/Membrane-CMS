create table InstalledPlugin (
	Id UniqueIdentifier primary key clustered not null,
	Name nvarchar(255) not null,
	Version nvarchar(255) not null
)

insert into InstalledPlugin (Id, Name, Version)
values (NewId(), 'News', '1.0.0')