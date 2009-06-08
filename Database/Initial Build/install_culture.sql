create table Culture (
	Id UniqueIdentifier primary key clustered not null,
	[Language] nvarchar(255) not null,
	ShortCode char(2) not null,
	IsDefault bit
)

insert into Culture (Id, [Language], ShortCode, [IsDefault])
values (NewId(), 'English', 'EN', 1)