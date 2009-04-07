using System.Linq;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.CRUD.Queries
{
	public class PagedItems<T> : IQueryCommand<T> where T : IEntity
	{
		private readonly int skip;
		private readonly int take;

		public PagedItems(int skip, int take)
		{
			this.skip = skip;
			this.take = take;
		}

		public IQueryable<T> Execute(IRepository<T> repository)
		{
			return repository.AsQueryable().Skip(skip).Take(take);
		}
	}
}