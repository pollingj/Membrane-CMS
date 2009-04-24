using System;
using System.Collections.Generic;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.Services;

namespace Membrane.Commons.Plugin.Controllers
{
	public class OrderCRUDController<TDto, TEntity> : CRUDController<TDto, TEntity>, IOrderCRUDController<TDto, TEntity>
		where TDto : IDto
		where TEntity : IEntity
	{
		private readonly IOrderCRUDService<TDto, TEntity> OrderService;
		private readonly IPropertyReaderService<TDto> propertyReaderService;

		public OrderCRUDController(IOrderCRUDService<TDto, TEntity> service, IPropertyReaderService<TDto> propertyReaderService) : base(service, propertyReaderService)
		{
			this.propertyReaderService = propertyReaderService;
			Service = OrderService = service;

			ListView = @"\Shared\OrderedList";
		}


		public void MoveItemDown(Guid id)
		{
			Flash["items"] = OrderService.MoveItemDown(id, (IList<TDto>)Flash["items"]);
			RenderView(ListView);
		}

		public void MoveItemUp(Guid id)
		{
			Flash["items"] = OrderService.MoveItemUp(id, (IList<TDto>)Flash["items"]);
			RenderView(ListView);
		}

		public void SaveOrder()
		{
			var success = OrderService.SaveItemsOrder((IList<TDto>)Flash["items"]);

			if (!success)
				CreateError("Could not save list order");

			RedirectToAction("List");
		}
	}
}