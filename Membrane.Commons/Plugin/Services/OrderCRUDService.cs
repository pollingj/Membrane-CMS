using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Commons.Plugin.Services
{
	public class OrderCRUDService<TDto, TEntity> : CRUDService<TDto, TEntity>, IOrderCRUDService<TDto, TEntity>
		where TDto : IOrderedDto
		where TEntity : IOrderedEntity
	{
		public OrderCRUDService(IRepository<TEntity> repository) : base(repository)
		{
		}

		/// <summary>
		/// Moves a specified item down one level in the list
		/// </summary>
		/// <param name="currentItemsOrder">The current list of items</param>
		/// <param name="id">The id of the item to be moved</param>
		/// <returns>A newly ordered list of items</returns>
		public ItemOrderResponseDTO MoveItemDown(ItemOrderRequestDTO currentItemsOrder, Guid id)
		{
			var listPosition = getListPosition(currentItemsOrder, id);

			return swapListPositions(currentItemsOrder, listPosition, listPosition + 1);
		}



		/// <summary>
		/// Moves a specified item Up one level in the list
		/// </summary>
		/// <param name="currentItemsOrder">The current list of items</param>
		/// <param name="id">The id of the item to be moved</param>
		/// <returns>A newly ordered list of items</returns>
		public ItemOrderResponseDTO MoveItemUp(ItemOrderRequestDTO currentItemsOrder, Guid id)
		{
			var listPosition = getListPosition(currentItemsOrder, id);

			return swapListPositions(currentItemsOrder, listPosition, listPosition - 1);
		}

		/// <summary>
		/// Saves the current order of list items
		/// </summary>
		/// <param name="currentItemsOrder">The current list items in the correct order</param>
		/// <returns>If successful or not (bool)</returns>
		public bool SaveItemsOrder(ItemOrderRequestDTO currentItemsOrder)
		{
			var success = true;
			var positionCount = 1;
			try
			{
				foreach (var id in currentItemsOrder.Ids)
				{
					var item = Repository.FindById(id);
					item.OrderPosition = positionCount;
					Repository.Update(item);

					positionCount++;
				}
			}
			catch (Exception)
			{
				success = false;
			}
			return success;
		}

		private int getListPosition(ItemOrderRequestDTO currentItemsOrder, Guid id)
		{
			var pos = -1;
			var count = 0;
			foreach (var item in currentItemsOrder.Ids)
			{
				if (item == id)
				{
					pos = count;
					break;
				}
				count++;
			}

			return pos;
		}

		private ItemOrderResponseDTO swapListPositions(ItemOrderRequestDTO currentItemsOrder, int listPosition, int newPosition)
		{
			GuardAgainst.ArgumentOutsideRange(listPosition, "List Position", 0, currentItemsOrder.Ids.Length - 1);

			var newOrderedList = new List<Guid>(currentItemsOrder.Ids);
			newOrderedList[listPosition] = currentItemsOrder.Ids[newPosition];
			newOrderedList[newPosition] = currentItemsOrder.Ids[listPosition];

			return new ItemOrderResponseDTO { Ids = newOrderedList.ToArray() };
		}
	}
}