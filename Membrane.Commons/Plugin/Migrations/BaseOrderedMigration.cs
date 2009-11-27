using System.Collections.Generic;
using System.Data;
using Migrator.Framework;

namespace Membrane.Commons.Plugin.Migrations
{
	public class BaseOrderedMigration : BaseMigration
	{
		public Column[] Columns
		{
			get
			{
				var columns = new List<Column>(base.Columns);
                columns.Add(new Column("OrderPosition", DbType.Int32, ColumnProperty.NotNull));
				return columns.ToArray();
			}
		}
	}
}