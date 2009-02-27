using Membrane.Commons.Web;
using NHibernate;

namespace Membrane.Commons.Persistence.NHibernate
{
	public class NHibernateHttpContextSessionLocater : INHibernateSessionLocater
	{
		private readonly IHttpContext httpContext;
		private const string SESSION_KEY = "NHibernateWebSessionModule.session";

		public NHibernateHttpContextSessionLocater(IHttpContext httpContext)
		{
			this.httpContext = httpContext;
		}

		public ISession CurrentSession
		{
			get
			{
				var session = httpContext.GetItem(SESSION_KEY) as ISession;

				if (session == null)
					throw new SessionException(string.Format("Unable to locate an ISession in the Http Context at item key '{0}'.", SESSION_KEY));

				return session;
			}
		}
	}

}