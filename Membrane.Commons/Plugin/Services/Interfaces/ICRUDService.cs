using System;
using System.Collections.Generic;

namespace Membrane.Commons.Plugin.Services.Interfaces
{
	public interface ICRUDService<TDto, TEntity>
	{
		IList<TDto> GetPagedItems(int currentPage, int pageSize);
		IList<TDto> GetItems();
		TDto GetItem(Guid id);
		Guid Create(TDto group);
		bool Update(TDto group);
		bool Delete(Guid id);
		
	}
}