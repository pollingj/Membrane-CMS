using System;
using Castle.Core.Interceptor;
using Castle.Core.Logging;
using Castle.Services.Transaction;

namespace Membrane.Commons.Persistence.NHibernate
{
	public class NHibernateTransactionInterceptor : IInterceptor
	{
		private readonly INHibernateSessionLocater sessionLocater;
		private readonly ILogger logger;

		public NHibernateTransactionInterceptor(INHibernateSessionLocater sessionLocater, ILogger logger)
		{
			GuardAgainst.ArgumentNull(sessionLocater, "sessionLocater");
			GuardAgainst.ArgumentNull(logger, "logger");

			this.sessionLocater = sessionLocater;
			this.logger = logger;
		}

		public void Intercept(IInvocation invocation)
		{
			GuardAgainst.ArgumentNull(invocation, "invocation");

			logger.Info("Intercepting transactional component {0}, checking method {1} requires a transaction...", invocation.TargetType.Name, invocation.Method.Name);

			bool transactionRequired = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(TransactionAttribute), false).Length != 0;

			if (transactionRequired)
			{
				//The method is marked as transaction required. Each request has a transaction started by default (in NHibernateWebSessionModule)
				//so this handles the transaction rollback only

				try
				{
					invocation.Proceed();
				}
				catch (Exception)
				{
					logger.Info("Method {0}.{1} threw an exception, rolling back transaction.", invocation.TargetType.Name, invocation.Method.Name);
					sessionLocater.CurrentSession.Transaction.Rollback();
					throw;
				}
			}
			else
			{
				//The method is not marked as transaction required and so the default transaction will not be rolled back even if an exception occurs

				logger.Info("Method {0}.{1} does not require a transaction, executing as normal (exceptions will not rollback the default transaction).", invocation.TargetType.Name, invocation.Method.Name);
				invocation.Proceed();
			}
		}
	}

}