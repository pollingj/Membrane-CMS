using System;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.Services;

namespace Membrane.Commons.Plugin.Controllers
{
	public class OrderCRUDController<TDto, TEntity> : CRUDController<TDto, TEntity>, IOrderCRUDController<TDto, TEntity>
		where TDto : IOrderedDto
		where TEntity : IOrderedEntity
	{
		private readonly IOrderCRUDService<TDto, TEntity> service;

		public OrderCRUDController(IOrderCRUDService<TDto, TEntity> service, IPropertyReaderService<TDto> propertyReaderService) : base(service, propertyReaderService)
		{
			this.service = service;

			ListView = @"\Shared\OrderedList";
		}


		public void MoveItemDown(ItemOrderRequestDTO currentOrder, Guid id)
		{
			PropertyBag["items"] = service.MoveItemDown(currentOrder, id);
			RenderView(ListView);
		}

		public void MoveItemUp(ItemOrderRequestDTO currentOrder, Guid id)
		{
			PropertyBag["items"] = service.MoveItemUp(currentOrder, id);
			RenderView(ListView);
		}

		public void SaveOrder(ItemOrderRequestDTO currentOrder)
		{
			var success = service.SaveItemsOrder(currentOrder);

			if (!success)
				CreateError("Could not save list order");

			RedirectToAction("List");
		}
	}
}