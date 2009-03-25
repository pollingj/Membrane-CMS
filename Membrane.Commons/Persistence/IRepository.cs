using System;
using System.Collections.Generic;
using System.Linq;

namespace Membrane.Commons.Persistence
{
	public interface IRepository<T> where T : IEntity
	{
		T FindById(Guid id);
		void Save(T entity);
		IQueryable<T> AsQueryable();
		ICollection<T> Find(IQueryCommand<T> queryCommand);
		T FindOne(IQueryCommand<T> queryCommand);

	}

}