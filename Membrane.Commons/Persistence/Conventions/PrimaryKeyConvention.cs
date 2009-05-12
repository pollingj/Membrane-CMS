using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace Membrane.Commons.Persistence.Conventions
{
	public class PrimaryKeyConvention : IIdConvention
	{
		public bool Accept(IIdentityPart target)
		{
			return true;
		}

		public void Apply(IIdentityPart target)
		{
			target.ColumnName("Id");
		}
	}
}
