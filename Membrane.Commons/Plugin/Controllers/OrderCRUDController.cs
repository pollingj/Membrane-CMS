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
		private readonly IPropertyReaderService<TDto> propertyReaderService;

		public OrderCRUDController(ICRUDService<TDto, TEntity> service, IPropertyReaderService<TDto> propertyReaderService) : base(service, propertyReaderService)
		{
			this.propertyReaderService = propertyReaderService;
			Service = service;

			ListView = @"\Shared\OrderedList";
		}


		public void MoveItemDown(Guid id)
		{
			Flash["items"] = Service.MoveItemDown(id, (IList<TDto>)Flash["items"]);
		}

		public void MoveItemUp(Guid id)
		{
			Flash["items"] = Service.MoveItemUp(id, (IList<TDto>)Flash["items"]);
		}

		public void SaveOrder()
		{
			var success = Service.SaveItemsOrder((IList<TDto>)Flash["items"]);

			if (!success)
				CreateError("Could not save list order");

			RedirectToAction("List");
		}
	}
}