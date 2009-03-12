using System.Collections.Generic;
using Castle.Components.Pagination;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.Services
{
	public interface IBaseCrudService<T> : IService where T : IEntity
	{
		ICollection<T> GetAllData();
		IPaginatedPage<T> GetPagedData(int currentPage, int recordsPerPage);
		T GetItem(int id);
		T Create(T item);
		bool Update(T item);
		bool Delete(int id);
	}
}