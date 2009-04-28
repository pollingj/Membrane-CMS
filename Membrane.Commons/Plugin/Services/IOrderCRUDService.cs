using System;
using System.Collections.Generic;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Commons.Plugin.Services
{
	public interface IOrderCRUDService<TDto, TEntity> : ICRUDService<TDto, TEntity>
	{
		IList<TDto> MoveItemDown(IList<TDto> currentItemOrder, Guid id);
		IList<TDto> MoveItemUp(IList<TDto> currentItemOrder, Guid id);
		bool SaveItemsOrder(ItemOrderRequestDTO currentItemOrder);
	}
}