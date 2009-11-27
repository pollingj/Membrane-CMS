using NHibernate;

namespace Membrane.Commons.Persistence.NHibernate
{
	public interface INHibernateSessionLocater
	{
		ISession CurrentSession { get; }
	}

}