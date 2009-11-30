using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Membrane.Commons.Persistence.Conventions
{
	public class TableNameConvention : IClassConvention
	{
		public void Apply(IClassInstance target)
		{
			target.Table(target.EntityType.Name);
		}
	}
}