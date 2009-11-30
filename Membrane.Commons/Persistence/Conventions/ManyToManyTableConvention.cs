using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Membrane.Commons.Persistence.Conventions
{
	public class ManyToManyTableConvention : IHasManyToManyConvention
	{
		public void Apply(IManyToManyCollectionInstance target)
		{
			target.Table(target.ChildType.Name + "_To_" + target.EntityType.Name);
		}
	}
}