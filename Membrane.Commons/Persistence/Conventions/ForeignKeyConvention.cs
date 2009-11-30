using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Membrane.Commons.Persistence.Conventions
{
	public class ForeignKeyConvention : IHasManyConvention
	{
		public void Apply(IOneToManyCollectionInstance target)
		{
			target.Key.Column(target.EntityType.Name + "_Id");
		}
	}
}