using System;
using System.Collections.Generic;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.Services;

namespace Membrane.Commons.Plugin.Controllers
{
	public class OrderCRUDController<TDto, TEntity> : CRUDController<TDto, TEntity>, IOrderCRUDController<TDto, TEntity>
		where TDto : IOrderedDto
		where TEntity : IOrderedEntity
	{
		private readonly IOrderCRUDService<TDto, TEntity> service;
		private readonly IPropertyReaderService<TDto> propertyReaderService;

		public OrderCRUDController(IOrderCRUDService<TDto, TEntity> service, IPropertyReaderService<TDto> propertyReaderService) : base(service, propertyReaderService)
		{
			this.propertyReaderService = propertyReaderService;
			this.service = service;

			ListView = @"\Shared\OrderedList";
		}


		public void MoveItemDown(Guid id)
		{
			Flash["items"] = service.MoveItemDown(id, (IList<TDto>)Flash["items"]);
			RenderView(ListView);
		}

		public void MoveItemUp(Guid id)
		{
			Flash["items"] = service.MoveItemUp(id, (IList<TDto>)Flash["items"]);
			RenderView(ListView);
		}

		public void SaveOrder()
		{
			var success = service.SaveItemsOrder((IList<TDto>)Flash["items"]);

			if (!success)
				CreateError("Could not save list order");

			RedirectToAction("List");
		}
	}
}