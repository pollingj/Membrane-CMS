using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plug;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.DTOs.Interfaces;
using Membrane.Commons.Plugin.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Commons.Plugin.Controllers
{
	public class OrderCRUDControllerFixture<TDto, TEntity> : CRUDControllerFixture<TDto, TEntity>
		where TDto : IOrderedDto
		where TEntity : IOrderedEntity
	{
		private OrderCRUDController<TDto, TEntity> controller;
		private IOrderCRUDService<TDto, TEntity> service;
		public ItemOrderRequestDTO OrderedList { get; set; }

		public IOrderCRUDService<TDto, TEntity> OrderedService
		{
			get { return service; }
			set { Service = service = value; }
		}

		public OrderCRUDController<TDto, TEntity> Controller
		{
			get { return controller; }
			set { base.Controller = controller = value; }
		}

		

		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();
			OrderedService = mockery.DynamicMock<IOrderCRUDService<TDto, TEntity>>();
			ListView = "/Shared/OrderedList";
		}

		[Test]
		public virtual void CanSuccessfullyMoveTopItemDownOnePlace()
		{
			var newOrder = new List<TDto>(ListDTO);

			newOrder[0] = ListDTO[1];
			newOrder[1] = ListDTO[0];

			MockMovingItemsWithinList(newOrder, ListDTO[0].Id);
		}

		[Test]
		public virtual void CanSuccessfullyMoveBottomItemUpOnePlace()
		{
			var newOrder = new List<TDto>(ListDTO);

			newOrder[2] = ListDTO[3];
			newOrder[3] = ListDTO[2];

			MockMovingItemsWithinList(newOrder, ListDTO[3].Id);
		}

		[Test]
		public virtual void CanSuccessfullySaveCurrentOrder()
		{
			MockSavingItemOrder(true);

			Assert.IsNull(Controller.Flash["error"]);
			Assert.AreEqual(@"/Controller/List.castle", Response.RedirectedTo);
		}

		[Test]
		public virtual void CanFailSaveCurrentOrder()
		{
			MockSavingItemOrder(false);

			Assert.IsNotNull(Controller.Flash["error"]);
			Assert.AreEqual(@"/Controller/List.castle", Response.RedirectedTo);
		}

		private void MockSavingItemOrder(bool success)
		{
			Controller.Flash["items"] = OrderedList;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.SaveItemsOrder(OrderedList)).Return(success))
				.Verify(() => Controller.SaveOrder(OrderedList));
		}

		private void MockMovingItemsWithinList(List<TDto> newOrder, Guid id)
		{
			Controller.Flash["items"] = ListDTO;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.MoveItemUp(ListDTO, id)).Return(newOrder))
				.Verify(() => Controller.MoveItemUp(id));

			Assert.AreNotEqual(OrderedList, Controller.Flash["items"]);
			Assert.AreEqual(newOrder, Controller.Flash["items"]);
		}
	}
}