using System.Collections.Generic;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.Services
{
	public interface IBaseCrudService<T> : IService where T : IEntity
	{
		ICollection<T> GetAllData();

	}
}