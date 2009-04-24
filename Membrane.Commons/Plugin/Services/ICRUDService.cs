using System;
using System.Collections.Generic;

namespace Membrane.Commons.Plugin.Services
{
	public interface ICRUDService<TDto, TEntity>
	{
		void RegisterMappings();
		IList<TDto> GetPagedItems(int currentPage, int pageSize);
		IList<TDto> GetItems();
		TDto GetItem(Guid id);
		Guid Create(TDto group);
		bool Update(TDto group);
		bool Delete(Guid id);
		IList<TDto> MoveItemDown(Guid guid, IList<TDto> items);
		IList<TDto> MoveItemUp(Guid guid, IList<TDto> items);
		bool SaveItemsOrder(IList<TDto> items);
	}
}