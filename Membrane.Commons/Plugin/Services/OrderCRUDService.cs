using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plug;
using Membrane.Commons.Plugin.DTOs.Interfaces;
using Membrane.Commons.Plugin.Queries;
using Membrane.Commons.Plugin.Services.Interfaces;

namespace Membrane.Commons.Plugin.Services
{
	public class OrderCRUDService<TDto, TEntity> : CRUDService<TDto, TEntity>, IOrderCRUDService<TDto, TEntity>
		where TDto : IOrderedDto
		where TEntity : IOrderedEntity
	{
		public OrderCRUDService(IRepository<TEntity> repository) : base(repository)
		{
		}

		public override IList<TDto> GetPagedItems(int currentPage, int pageSize)
		{
			var skip = 0;

			if (currentPage > 1)
				skip = pageSize * (currentPage - 1);

			var items = Repository.Find(new OrderedPagedItems<TEntity>(skip, pageSize));
			items = items.AsQueryable().Skip(skip).Take(pageSize).OrderBy(t => t.OrderPosition).ToList();
			return Mapper.Map<ICollection<TEntity>, IList<TDto>>(items);
		}

		/// <summary>
		/// Moves a specified item down one level in the list
		/// </summary>
		/// <param name="currentItemsOrder">The current list of items</param>
		/// <param name="id">The id of the item to be moved</param>
		/// <returns>A newly ordered list of items</returns>
		public virtual IList<TDto> MoveItemDown(IList<TDto> currentItemsOrder, Guid id)
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
		public virtual IList<TDto> MoveItemUp(IList<TDto> currentItemsOrder, Guid id)
		{
			var listPosition = getListPosition(currentItemsOrder, id);

			return swapListPositions(currentItemsOrder, listPosition, listPosition - 1);
		}

		/// <summary>
		/// Saves the current order of list items
		/// </summary>
		/// <param name="currentItemsOrder">The current list items in the correct order</param>
		/// <returns>If successful or not (bool)</returns>
		public virtual bool SaveItemsOrder(ItemOrderRequestDTO currentItemsOrder)
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
			catch (Exception ex)
			{
				success = false;
			}
			return success;
		}

		private int getListPosition(IList<TDto> currentItemsOrder, Guid id)
		{
			var pos = -1;
			var count = 0;
			foreach (var item in currentItemsOrder)
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

		private IList<TDto> swapListPositions(IList<TDto> currentItemsOrder, int listPosition, int newPosition)
		{
			GuardAgainst.ArgumentOutsideRange(listPosition, "List Position", 0, currentItemsOrder.Count-1);

			var newOrderedList = new List<TDto>(currentItemsOrder);
			newOrderedList[listPosition] = currentItemsOrder[newPosition];
			newOrderedList[newPosition] = currentItemsOrder[listPosition];

			return newOrderedList;
		}
	}
}