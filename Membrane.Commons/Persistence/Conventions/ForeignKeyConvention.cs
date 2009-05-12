using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Membrane.Commons.Persistence.Conventions
{
	public class ForeignKeyConvention : IHasManyConvention
	{
		public bool Accept(IOneToManyPart target)
		{
			return true;
		}

		public void Apply(IOneToManyPart target)
		{
			target.KeyColumnNames.Clear();
			target.KeyColumnNames.Add(target.EntityType.Name + "_Id"); 
		}
	}
}