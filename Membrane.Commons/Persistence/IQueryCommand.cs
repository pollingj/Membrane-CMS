using System.Linq;

namespace Membrane.Commons.Persistence
{
	public interface IQueryCommand<T> where T : IEntity
	{
		IQueryable<T> Execute(IRepository<T> repository);
	}

}