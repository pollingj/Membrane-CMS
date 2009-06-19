create table NewsArticle (
	Id UniqueIdentifier primary key clustered not null,
	Name nvarchar(255) not null,
	Story text not null,
	CreationDate DateTime not null,
	Culture_Id UniqueIdentifier not null,
	Revision int not null,
	ParentEntity_Id UniqueIdentifier,
	Published bit not null,
)