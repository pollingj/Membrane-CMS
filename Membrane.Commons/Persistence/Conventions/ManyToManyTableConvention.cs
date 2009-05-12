using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Membrane.Commons.Persistence.Conventions
{
	public class ManyToManyTableConvention : IHasManyToManyConvention
	{
		public bool Accept(IManyToManyPart target)
		{
			return true;
		}

		public void Apply(IManyToManyPart target)
		{
			target.WithTableName(target.ChildType.Name + "_To_" + target.EntityType.Name);
		}
	}
}