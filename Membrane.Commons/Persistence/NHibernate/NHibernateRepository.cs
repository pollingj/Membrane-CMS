using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;

namespace Membrane.Commons.Persistence.NHibernate
{
	public class NHibernateRepository<T> : IRepository<T> where T : class, IEntity
	{
		private readonly INHibernateSessionLocater sessionLocater;

		public NHibernateRepository(INHibernateSessionLocater sessionLocater)
		{
			GuardAgainst.ArgumentNull(sessionLocater, "sessionLocater");

			this.sessionLocater = sessionLocater;
		}

		public T FindById(int id)
		{
			return sessionLocater.CurrentSession.Load<T>(id);
		}

		public T Save(T entity)
		{
			GuardAgainst.ArgumentNull(entity, "entity");

			return (T)sessionLocater.CurrentSession.Save(entity);
		}

		public IQueryable<T> AsQueryable()
		{
			return sessionLocater.CurrentSession.Linq<T>().AsQueryable();
		}

		/*public ICollection<T> Find(IQueryCommand<T> queryCommand)
		{
			GuardAgainst.ArgumentNull(queryCommand, "queryCommand");

			return queryCommand.Execute(this).ToList();
		}

		public T FindOne(IQueryCommand<T> queryCommand)
		{
			GuardAgainst.ArgumentNull(queryCommand, "queryCommand");

			IList<T> entities = queryCommand.Execute(this).ToList();

			return entities.Count > 0 ? entities[0] : null;
		}*/

		public ICollection<T> Find()
		{
			return Find(null);
		}

		public ICollection<T> Find(IQueryable<T> queryable)
		{
			GuardAgainst.ArgumentNull(queryable, "IQueryable");

			return queryable.ToList();
		}

		public T FindOne(IQueryable<T> queryable)
		{
			GuardAgainst.ArgumentNull(queryable, "IQueryable");

			IList<T> entities = queryable.ToList();

			return entities.Count > 0 ? entities[0] : null;
		}

		public object FindOne(string query, Type type)
		{
			var entities = sessionLocater.CurrentSession.CreateSQLQuery(query).List();

			return entities.Count > 0 ? entities[0] : null;
		}

	}

}