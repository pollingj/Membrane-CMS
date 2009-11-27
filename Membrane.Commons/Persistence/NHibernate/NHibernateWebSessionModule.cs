using System;
using System.Data;
using System.Web;
using Castle.Windsor;
using NHibernate;

namespace Membrane.Commons.Persistence.NHibernate
{
	public class NHibernateWebSessionModule : IHttpModule
	{
		private const string SESSION_KEY = "NHibernateWebSessionModule.session";

		public void Init(HttpApplication context)
		{
			context.BeginRequest += OnBeginRequest;
			context.EndRequest += OnEndRequest;
		}

		private void OnBeginRequest(object sender, EventArgs e)
		{
			var application = sender as IContainerAccessor;
			if (application == null)
				throw new InvalidOperationException("The HttpApplication does not implement IContainerAccessor and therefore is not compatible.");

			var sessionRequiredSpecification = new NHibernateWebSessionRequiredSpecification();
			if (sessionRequiredSpecification.IsSatisfiedBy(HttpContext.Current.Request.Url))
			{
				var sessionFactory = application.Container.Resolve<ISessionFactory>();

				ISession session = sessionFactory.OpenSession();
				session.FlushMode = FlushMode.Commit;
				session.Transaction.Begin(IsolationLevel.ReadCommitted);    //To avoid implicit transactions, begin a transaction per request

				HttpContext.Current.Items.Add(SESSION_KEY, session);
			}
		}

		private void OnEndRequest(object sender, EventArgs e)
		{
			var session = HttpContext.Current.Items[SESSION_KEY] as ISession;
			if (session != null)
			{
				if (session.Transaction.IsActive && !session.Transaction.WasRolledBack)
				{
					//If the transaction wasn't rolled back by NHibernateTransactionInterceptor, commit it at the end of the request
					session.Transaction.Commit();
				}

				session.Dispose();
				HttpContext.Current.Items[SESSION_KEY] = null;
			}
		}

		public void Dispose()
		{
		}
	}

}