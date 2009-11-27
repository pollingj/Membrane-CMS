using System.Collections.Generic;
using System.Data;
using Migrator.Framework;

namespace Membrane.Commons.Plugin.Migrations
{
	public class BaseMigration : Migration
	{
		public List<Column> Columns
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

		public virtual void DropTable(string tableName)
		{
			Database.RemoveTable("NewsArticle");
		}

		public virtual void CreateTable(string tableName, List<Column> columns)
		{
			var allColumns = new List<Column>(Columns);
			allColumns.AddRange(columns);
			Database.AddTable(tableName, allColumns.ToArray());
		}

		public override void Up()
		{
				
		}

		public override void Down()
		{
		}
	}
}