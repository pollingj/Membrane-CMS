using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.Services.Transaction;

namespace Membrane.Commons.Persistence.NHibernate
{
	public class NHibernateTransactionContributer : IContributeComponentModelConstruction
	{
		public void ProcessModel(IKernel kernel, ComponentModel model)
		{
			GuardAgainst.ArgumentNull(kernel, "kernel");
			GuardAgainst.ArgumentNull(model, "model");

			if (model.Implementation.IsDefined(typeof(TransactionalAttribute), true))
			{
				model.Interceptors.AddIfNotInCollection(new InterceptorReference(typeof(NHibernateTransactionInterceptor)));
			}
		}
	}

}