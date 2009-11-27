using System.Linq;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.Plugin.Queries
{
	public class QueryItems<T> : IQueryCommand<T> where T : IEntity
	{

		public QueryItems()
		{

		}

		public IQueryable<T> Execute(IRepository<T> repository)
		{
			return repository.AsQueryable();
		}
	}
}