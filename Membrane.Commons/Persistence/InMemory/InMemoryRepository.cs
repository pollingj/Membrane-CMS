using System;
using System.Collections.Generic;
using System.Linq;

namespace Membrane.Commons.Persistence.InMemory
{
	public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
	{
		private readonly ICollection<T> entities;

		public InMemoryRepository(ICollection<T> entities)
		{
			GuardAgainst.ArgumentNull(entities, "entities");

			this.entities = entities;
		}

		public T FindById(Guid id)
		{
			GuardAgainst.ArgumentEmpty(id, "id");

			List<T> matches = entities.Where(e => e.Id == id).ToList();

			return matches.Count > 0 ? matches[0] : null;
		}

		public void Save(T entity)
		{
			GuardAgainst.ArgumentNull(entity, "entity");

			entities.Add(entity);
		}

		public void Update(T entity)
		{
			GuardAgainst.ArgumentNull(entity, "entity");

			T removeItem = FindById(entity.Id);

			if (removeItem != null)
				entities.Remove(removeItem);
			entities.Add(entity);
		}

		public IQueryable<T> AsQueryable()
		{
			return entities.AsQueryable();
		}

		public ICollection<T> Find(IQueryCommand<T> queryCommand)
		{
			GuardAgainst.ArgumentNull(queryCommand, "queryCommand");

			return queryCommand.Execute(this).ToList();
		}

		public T FindOne(IQueryCommand<T> queryCommand)
		{
			GuardAgainst.ArgumentNull(queryCommand, "queryCommand");

			List<T> matches = queryCommand.Execute(this).ToList();

			return matches.Count > 0 ? matches[0] : null;
		}

		public void Delete(Guid id)
		{
			GuardAgainst.ArgumentEmpty(id, "id");

			T removeItem = FindById(id);

			if (removeItem != null)
				entities.Remove(removeItem);
		}
	}

}