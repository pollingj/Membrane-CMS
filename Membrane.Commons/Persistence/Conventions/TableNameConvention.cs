using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Membrane.Commons.Persistence.Conventions
{
	public class TableNameConvention : IClassConvention
	{
		public bool Accept(IClassMap target)
		{
			return true;
		}

		public void Apply(IClassMap target)
		{
			target.WithTable(target.EntityType.Name);
		}
	}
}