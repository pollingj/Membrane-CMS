using System;
using System.Collections.Generic;
using System.Linq;

namespace Membrane.Commons.Persistence
{
	public interface IRepository<T> where T : IEntity
	{
		T FindById(int id);
		T Save(T entity);
		ICollection<T> Find();
		IQueryable<T> AsQueryable();
		/*ICollection<T> Find(IQueryCommand<T> queryCommand);
		T FindOne(IQueryCommand<T> queryCommand);*/
		ICollection<T> Find(IQueryable<T> queryCommand);
		T FindOne(IQueryable<T> queryCommand);
		object FindOne(string query, Type type);
	}

}