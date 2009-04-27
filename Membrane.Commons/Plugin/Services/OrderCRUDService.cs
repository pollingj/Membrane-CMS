using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;

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
		/// <param name="id">The id of the item to be moved</param>
		/// <param name="items">The current list of items</param>
		/// <returns>A newly ordered list of items</returns>
		public IList<TDto> MoveItemDown(Guid id, IList<TDto> items)
		{
			var listPosition = getListPosition(id, items);

			return swapListPositions(items, listPosition, listPosition + 1);
		}



		/// <summary>
		/// Moves a specified item Up one level in the list
		/// </summary>
		/// <param name="id">The id of the item to be moved</param>
		/// <param name="items">The current list of items</param>
		/// <returns>A newly ordered list of items</returns>
		public IList<TDto> MoveItemUp(Guid id, IList<TDto> items)
		{
			var listPosition = getListPosition(id, items);

			return swapListPositions(items, listPosition, listPosition - 1);
		}

		/// <summary>
		/// Saves the current order of list items
		/// </summary>
		/// <param name="items">The current list items in the correct order</param>
		/// <returns>If successful or not (bool)</returns>
		public bool SaveItemsOrder(IList<TDto> items)
		{
			var success = false;
			foreach (var item in items)
			{
				success = Update(item);
			}

			return success;
		}

		private int getListPosition(Guid id, IList<TDto> items)
		{
			var pos = -1;
			var count = 0;
			foreach (var item in items)
			{
				if (item.Id == id)
				{
					pos = count;
					break;
				}
				count++;
			}

			return pos;
		}

		private IList<TDto> swapListPositions(IList<TDto> items, int listPosition, int newPosition)
		{
			GuardAgainst.ArgumentOutsideRange(listPosition, "List Position", 0, items.Count - 1);

			var newOrderedList = new List<TDto>(items);
			newOrderedList[listPosition] = items[newPosition];
			newOrderedList[newPosition] = items[listPosition];

			return newOrderedList;
		}
	}
}