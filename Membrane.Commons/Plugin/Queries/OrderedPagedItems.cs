using System.Linq;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.Plugin.Queries
{
	public class OrderedPagedItems<T> : IQueryCommand<T> where T : IOrderedEntity
	{
		private readonly int skip;
		private readonly int take;

		public OrderedPagedItems(int skip, int take)
		{
			this.skip = skip;
			this.take = take;
		}

		public IQueryable<T> Execute(IRepository<T> repository)
		{
			return repository.AsQueryable();//.Skip(skip).Take(take).OrderBy(t => t.OrderPosition);
		}
	}
}