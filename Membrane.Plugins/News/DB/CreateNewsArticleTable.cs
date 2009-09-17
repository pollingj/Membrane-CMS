using System;
using System.Data;
using Migrator.Framework;

namespace Membrane.Plugins.News.DB
{
	[Migration(1)]
	public class CreateNewsArticleTable : Migration
	{
		public override void Up()
		{
			Database.AddTable("NewsArticle",
			                  new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
			                  new Column("Name", DbType.String, 255, ColumnProperty.NotNull),
			                  new Column("Story", DbType.DateTime, ColumnProperty.Null),
							  new Column("CreationDate", DbType.DateTime, ColumnProperty.NotNull),
							  new Column("Culture_Id", DbType.Guid, ColumnProperty.NotNull),
							  new Column("Revision", DbType.Int32, ColumnProperty.NotNull),
							  new Column("ParentEntity_Id", DbType.Guid, ColumnProperty.Null),
							  new Column("Published", DbType.Boolean, ColumnProperty.Null));
		}

		public override void Down()
		{
			Database.RemoveTable("NewsArticle");
		}
	}
}