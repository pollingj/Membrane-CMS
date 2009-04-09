using System;
using System.Collections.Generic;

namespace Membrane.Commons.CRUD.Services
{
	public interface ICRUDService<TDto, TEntity>
	{
		void RegisterMappings();
		IList<TDto> GetPagedItems(int currentPage, int pageSize);
		TDto GetItem(Guid id);
		Guid Create(TDto group);
		bool Update(TDto group);
		bool Delete(Guid id);
	}
}