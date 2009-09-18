using System.Collections.Generic;
using System.Data;
using Membrane.Commons.Plugin.Migrations;
using Migrator.Framework;

namespace Membrane.Plugins.News.DB
{
	[Migration(1)]
	public class CreateNewsArticleTable : BaseVersionedMigration
	{
		public override void Up()
		{
			var columns = new List<Column>();
			columns.Add(new Column("Story", DbType.DateTime, ColumnProperty.Null));
			columns.Add(new Column("CreationDate", DbType.DateTime, ColumnProperty.NotNull));
			CreateTable("NewsArticle", columns);
		}

		public override void Down()
		{
			DropTable("NewsArticle");
		}
	}
}