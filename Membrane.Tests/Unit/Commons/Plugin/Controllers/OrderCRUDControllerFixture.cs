using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.Services;
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

		public ItemOrderRequestDTO OrderedList { get; set; }

		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();
			OrderedService = mockery.DynamicMock<IOrderCRUDService<TDto, TEntity>>();
			ListView = @"\Shared\OrderedList";
		}

		[Test]
		public virtual void CanSuccessfullyMoveTopItemDownOnePlace()
		{
			var newOrder = new ItemOrderResponseDTO { Ids = new List<Guid>(OrderedList.Ids).ToArray() };

			newOrder.Ids[0] = OrderedList.Ids[1];
			newOrder.Ids[1] = OrderedList.Ids[0];

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.MoveItemDown(OrderedList, OrderedList.Ids[0])).Return(newOrder))
				.Verify(() => Controller.MoveItemDown(OrderedList, OrderedList.Ids[0]));

			Assert.AreNotEqual(OrderedList, Controller.PropertyBag["items"]);
			Assert.AreEqual(newOrder, Controller.PropertyBag["items"]);
		}

		[Test]
		public virtual void CanSuccessfullyMoveBootomItemUpOnePlace()
		{
			var newOrder = new ItemOrderResponseDTO { Ids = new List<Guid>(OrderedList.Ids).ToArray() };

			newOrder.Ids[2] = OrderedList.Ids[3];
			newOrder.Ids[3] = OrderedList.Ids[2];

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.MoveItemUp(OrderedList, OrderedList.Ids[3])).Return(newOrder))
				.Verify(() => Controller.MoveItemUp(OrderedList, OrderedList.Ids[3]));

			Assert.AreNotEqual(OrderedList, Controller.PropertyBag["items"]);
			Assert.AreEqual(newOrder, Controller.PropertyBag["items"]);
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
	}
}