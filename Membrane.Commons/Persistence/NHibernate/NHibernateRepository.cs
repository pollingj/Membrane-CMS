using System;
using System.Collections.Generic;
using System.Linq;
using Membrane.Commons.Persistence.Exceptions;
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

		public T FindById(Guid id)
		{
			GuardAgainst.ArgumentEmpty(id, "id");

			return sessionLocater.CurrentSession.Load<T>(id);
		}

		public void Save(T entity)
		{
			GuardAgainst.ArgumentNull(entity, "entity");

			try
			{
				sessionLocater.CurrentSession.Save(entity);
			}
			catch
			{
				throw new RepositorySaveException();
			}

		}

		public void Update(T entity)
		{
			GuardAgainst.ArgumentNull(entity, "entity");

			try
			{
				sessionLocater.CurrentSession.Update(entity);
			}
			catch
			{
				throw new RepositoryUpdateException();
			}
		}

		public IQueryable<T> AsQueryable()
		{
			return sessionLocater.CurrentSession.Linq<T>().AsQueryable();
		}

        public ICollection<T> Find(IQueryCommand<T> queryCommand)
        {
            GuardAgainst.ArgumentNull(queryCommand, "queryCommand");

            return queryCommand.Execute(this).ToList();
        }

        public T FindOne(IQueryCommand<T> queryCommand)
        {
            GuardAgainst.ArgumentNull(queryCommand, "queryCommand");

            IList<T> entities = queryCommand.Execute(this).ToList();

            return entities.Count > 0 ? entities[0] : null;
        }

		public void Delete(Guid id)
		{
			GuardAgainst.ArgumentEmpty(id, "id");

			var item = FindById(id);

			try
			{
				sessionLocater.CurrentSession.Delete(item);
			}
			catch
			{
				throw new RepositoryDeleteException();
			}
		}

		public ICollection<T> FindAll()
		{
			return sessionLocater.CurrentSession.Linq<T>().ToList();
		}




	}
}