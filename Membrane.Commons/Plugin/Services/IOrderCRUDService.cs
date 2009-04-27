using System;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Commons.Plugin.Services
{
	public interface IOrderCRUDService<TDto, TEntity> : ICRUDService<TDto, TEntity>
	{
		ItemOrderResponseDTO MoveItemDown(ItemOrderRequestDTO currentItemOrder, Guid id);
		ItemOrderResponseDTO MoveItemUp(ItemOrderRequestDTO currentItemOrder, Guid id);
		bool SaveItemsOrder(ItemOrderRequestDTO currentItemOrder);
	}
}