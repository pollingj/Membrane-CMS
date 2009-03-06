using System.Collections.Generic;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.Services
{
	public interface IBaseCrudService<T> where T : IEntity
	{
		ICollection<T> GetAllData();

	}
}