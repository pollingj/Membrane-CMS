using System.Collections.Generic;
using Castle.Components.Pagination;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.Services
{
	public class BaseCrudService<T> : IBaseCrudService<T> where T : IEntity
	{
		private readonly IRepository<T> repository;

		public BaseCrudService(IRepository<T> repository)
		{
			this.repository = repository;
		}

		public ICollection<T> GetAllData()
		{
			return repository.FindAll();
		}

		public IPaginatedPage<T> GetPagedData(int currentPage, int recordsPerPage)
		{
			throw new System.NotImplementedException();
		}

		public T GetItem(int id)
		{
			throw new System.NotImplementedException();
		}

		public T Create(T item)
		{
			throw new System.NotImplementedException();
		}

		public bool Update(T item)
		{
			throw new System.NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}