create table NewsArticle (
	Id UniqueIdentifier primary key clustered not null,
	Name nvarchar(255) not null,
	Story text not null,
	CreationDate DateTime not null
)