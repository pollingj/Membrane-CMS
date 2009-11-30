using System;
using System.Collections.Generic;
using Castle.Components.Binder;
using Castle.MonoRail.Framework;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.DTOs.Interfaces;
using Membrane.Commons.Plugin.Services.Interfaces;

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

			ListView = "Shared/OrderedList";
		}

		public override void List(int currentPage, int pageSize)
		{
			Flash["items"] = service.GetPagedItems(currentPage, pageSize);

			RenderSharedView(ListView);
		}

		public void MoveItemDown(Guid id)
		{
			Flash["items"] = service.MoveItemDown((IList<TDto>)Flash["items"], id);
			RenderSharedView(ListView);
		}

		public void MoveItemUp(Guid id)
		{
			Flash["items"] = service.MoveItemUp((IList<TDto>)Flash["items"], id);
			RenderSharedView(ListView);
		}

		public void SaveOrder([DataBind("ordereditems")] ItemOrderRequestDTO currentOrder)
		{
			ErrorList errors = GetDataBindErrors(currentOrder);

			var success = service.SaveItemsOrder(currentOrder);

			if (!success)
				CreateError("Could not save list order");

			RedirectToAction("List");
		}
	}
}