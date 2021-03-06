create table NavigationType (
	Id UniqueIdentifier primary key clustered not null,
	Name nvarchar(255) not null
)

create table NavigationNode (
	Id UniqueIdentifier primary key clustered not null,
	Culture_Id UniqueIdentifier not null,
	Revision int not null,
	ParentEntity_Id UniqueIdentifier,
	Published bit not null,
	Type_Id UniqueIdentifier not null,
	Parent_Id UniqueIdentifier,
	Name nvarchar(255) not null,
	Title nvarchar(255) not null,
	AccessKey char(1),
	ExternalUrl nvarchar(255),
	OrderPosition int not null
)

alter table NavigationNode add constraint FK_NavigationNode_Culture foreign key (Culture_Id) references Culture
alter table NavigationNode add constraint FK_NavigationNode_NavigationType foreign key (Type_Id) references NavigationType
alter table NavigationNode add constraint FK_NavigationNode_ParentNavigationNode foreign key (Parent_Id) references NavigationNode
alter table NavigationNode add constraint FK_NavigationNode_ParentEntity foreign key (ParentEntity_Id) references NavigationNode

insert into NavigationType (Id, Name)
values (NewId(), 'Primary Navigation')

insert into NavigationNode (Id, Culture_Id, Revision, Published, Type_Id, Name, Title, AccessKey, OrderPosition)
select NewId(), (Select Id From Culture WHERE ShortCode = 'EN'), 1,  1, (Select Id From NavigationType WHERE Name = 'Primary Navigation'), 'Home',  'Visit the site home page', '1', 1
insert into NavigationNode (Id, Culture_Id, Revision, Published, Type_Id, Name, Title, AccessKey, OrderPosition)
select NewId(), (Select Id From Culture WHERE ShortCode = 'EN'), 1,  1, (Select Id From NavigationType WHERE Name = 'Primary Navigation'), 'About Us',  'Find out more about us', '2', 2
insert into NavigationNode (Id, Culture_Id, Revision, Published, Type_Id, Name, Title, AccessKey, OrderPosition)
select NewId(), (Select Id From Culture WHERE ShortCode = 'EN'), 1,  1, (Select Id From NavigationType WHERE Name = 'Primary Navigation'), 'Contact Us',  'Find out how to contact us', '3', 3