using System.Collections.Generic;
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
	}
}