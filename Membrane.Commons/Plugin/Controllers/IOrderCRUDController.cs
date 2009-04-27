using System;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Commons.Plugin.Controllers
{
	public interface IOrderCRUDController<TDto, TEntity> : ICRUDController<TDto, TEntity>
	{
		void MoveItemDown(ItemOrderRequestDTO currentOrder, Guid id);
		void MoveItemUp(ItemOrderRequestDTO currentOrder, Guid id);
		void SaveOrder(ItemOrderRequestDTO currentOrder);
	}
}