using Castle.MicroKernel.Facilities;

namespace Membrane.Commons.Persistence.NHibernate
{
	public class NHibernateTransactionFacility : AbstractFacility
	{
		protected override void Init()
		{
			Kernel.AddComponent("transaction.interceptor", typeof(NHibernateTransactionInterceptor));
			Kernel.ComponentModelBuilder.AddContributor(new NHibernateTransactionContributer());
		}
	}

}