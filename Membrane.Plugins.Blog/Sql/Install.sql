create table Post (
	Id UniqueIdentifier primary key clustered not null,
	Title nvarchar(255),
	Body nvarchar(max),
	Uri nvarchar(255)
)

insert into Post 
	(Id, Title, Body, Uri) 
values
	(NewId(), 'This is my first post about Membrane', 'This is the body of the post', 'my-first-post-about-membrane')