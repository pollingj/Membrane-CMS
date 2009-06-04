using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

		public Guid Save(T entity)
		{
			GuardAgainst.ArgumentNull(entity, "entity");
			Guid newId;
			cleanRelatedData(entity);

			try
			{
				newId = (Guid)sessionLocater.CurrentSession.Save(entity);
				sessionLocater.CurrentSession.Flush();
			}
			catch
			{
				throw new RepositorySaveException();
			}

			return newId;

		}

		public void Update(T entity)
		{
			GuardAgainst.ArgumentNull(entity, "entity");

			cleanRelatedData(entity);

			try
			{
				sessionLocater.CurrentSession.Update(entity);
				sessionLocater.CurrentSession.Flush();
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
				sessionLocater.CurrentSession.Flush();
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

		/// <summary>
		/// Looks through the related entity data and checks for an Guid.Empty value in the related entity id and sets the entity to null if required
		/// </summary>
		/// <remarks>This may be a little risky and may be better suited somewhere else, but works for the moment</remarks>
		/// <param name="item">The object to be cleaned</param>
		private void cleanRelatedData(IEntity item)
		{
			foreach (var property in item.GetType().GetProperties())
			{
				if (property.PropertyType.GetInterface("IEntity") != null)
				{
					var relatedObjectValues = property.GetValue(item, BindingFlags.Instance | BindingFlags.Public, null, null, null);
					if (relatedObjectValues != null && ((IEntity)relatedObjectValues).Id == Guid.Empty)
						property.SetValue(item, null, null);
				}
			}
		}


	}
}