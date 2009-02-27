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
				logger.Info("Starting transaction for method {0}.{1}.", invocation.TargetType.Name, invocation.Method.Name);

				sessionLocater.CurrentSession.Transaction.Begin();

				try
				{
					invocation.Proceed();

					logger.Info("Method {0}.{1} completed successfully, committing transaction.", invocation.TargetType.Name, invocation.Method.Name);
					sessionLocater.CurrentSession.Transaction.Commit();
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
				logger.Info("Method {0}.{1} does not require a transaction, executing as normal.", invocation.TargetType.Name, invocation.Method.Name);
				invocation.Proceed();
			}
		}
	}

}