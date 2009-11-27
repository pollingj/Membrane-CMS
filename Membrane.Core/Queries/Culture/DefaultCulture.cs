using System.Linq;
using Membrane.Commons.Persistence;

namespace Membrane.Core.Queries.Culture
{
	public class DefaultCulture : IQueryCommand<Commons.Plugin.Entities.Culture>
	{
		public IQueryable<Commons.Plugin.Entities.Culture> Execute(IRepository<Commons.Plugin.Entities.Culture> repository)
		{
			return repository.AsQueryable().Where(c => c.IsDefault);
		}
	}
}