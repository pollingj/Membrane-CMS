using System.Collections.Generic;
using System.Data;
using Migrator.Framework;

namespace Membrane.Commons.Plugin.Migrations
{
	public class BaseMigration : Migration
	{
		public IList<Column> Columns
		{
			get
			{
				return new List<Column>
				       	{
				       		new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
				       		new Column("Name", DbType.String, 255, ColumnProperty.NotNull)
				       	};
			}
		}

		public void DropTable(string tableName)
		{
			Database.RemoveTable("NewsArticle");
		}

		public override void Up()
		{
				
		}

		public override void Down()
		{
		}
	}
}