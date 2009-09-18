using System.Collections.Generic;
using System.Data;
using Migrator.Framework;

namespace Membrane.Commons.Plugin.Migrations
{
	public class BaseVersionedMigration : BaseMigration
	{
		public List<Column> Columns
		{
			get
			{
				var columns = new List<Column>(base.Columns);
				columns.Add(new Column("Culture_Id", DbType.Guid, ColumnProperty.NotNull));
				columns.Add(new Column("Revision", DbType.Int32, ColumnProperty.NotNull));
				columns.Add(new Column("ParentEntity_Id", DbType.Guid, ColumnProperty.Null));
				columns.Add(new Column("Published", DbType.Boolean, ColumnProperty.Null));
				return columns;
			}
		}

		public override void CreateTable(string tableName, List<Column> columns)
		{
			base.CreateTable(tableName, columns);
			Database.AddForeignKey(string.Format("FK_{0}_Culture", tableName), tableName, "Culture_Id", "Culture", "Id");
			Database.AddForeignKey(string.Format("FK_{0}_{0}", tableName), tableName, "ParentEntity_Id", tableName, "Id");
			
		}

		public override void DropTable(string tableName)
		{
			Database.RemoveForeignKey(tableName, string.Format("FK_{0}_Culture", tableName));
			Database.RemoveForeignKey(tableName, string.Format("FK_{0}_{0}", tableName));
			base.DropTable(tableName);
		}
	}
}