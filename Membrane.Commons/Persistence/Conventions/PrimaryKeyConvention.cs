using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Membrane.Commons.Persistence.Conventions
{
	public class PrimaryKeyConvention : IIdConvention
	{
		public void Apply(IIdentityInstance target)
		{
			target.Column("Id");
		}
	}
}
