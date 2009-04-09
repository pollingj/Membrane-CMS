create table NavigationType (
	Id UniqueIdentifier primary key clustered not null,
	Name nvarchar(255) not null
)

insert into NavigationType (Id, Name)
values (NewId(), 'Primary Navigation')