using System;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Commons.Plugin.Controllers
{
	public interface IOrderCRUDController<TDto, TEntity> : ICRUDController<TDto, TEntity>
	{
		void MoveItemDown(Guid id);
		void MoveItemUp(Guid id);
		void SaveOrder(ItemOrderRequestDTO currentOrder);
	}
}