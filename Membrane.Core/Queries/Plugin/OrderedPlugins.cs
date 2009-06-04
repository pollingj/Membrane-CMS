using System.Linq;
using Membrane.Commons.Persistence;
using Membrane.Entities;

namespace Membrane.Core.Queries.Plugin
{
	public class OrderedPlugins : IQueryCommand<InstalledPlugin>
	{
		public IQueryable<InstalledPlugin> Execute(IRepository<InstalledPlugin> repository)
		{
			return repository.AsQueryable().OrderBy(p => p.Name);
		}
	}
}