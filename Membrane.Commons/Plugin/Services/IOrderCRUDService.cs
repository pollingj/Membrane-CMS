using System;
using System.Collections.Generic;

namespace Membrane.Commons.Plugin.Services
{
	public interface IOrderCRUDService<TDto, TEntity> : ICRUDService<TDto, TEntity>
	{
		IList<TDto> MoveItemDown(Guid id, IList<TDto> items);
		IList<TDto> MoveItemUp(Guid id, IList<TDto> items);
		bool SaveItemsOrder(IList<TDto> items);
	}
}