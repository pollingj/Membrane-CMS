using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin;
using Membrane.Commons.Plugin.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Commons.Plugin.Controllers
{
	public class OrderCRUDControllerFixture<TDto, TEntity> : CRUDControllerFixture<TDto, TEntity>
		where TDto : IDto
		where TEntity : IEntity
	{
		private OrderCRUDController<TDto, TEntity> controller;
		public OrderCRUDController<TDto, TEntity> Controller
		{
			get { return controller; }
			set { base.Controller = controller = value; }
		}

		protected IList<TDto> OrderedList;

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();
		}

		[Test]
		public virtual void CanSuccessfullyMoveTopItemDownOnePlace()
		{
			var newOrder = new List<TDto>(OrderedList);
			Controller.Flash["items"] = OrderedList;

			newOrder[0] = OrderedList[1];
			newOrder[1] = OrderedList[0];

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(Service.MoveItemDown(OrderedList[0].Id, OrderedList)).Return(newOrder))
				.Verify(() => Controller.MoveItemDown(OrderedList[0].Id));

			Assert.AreNotEqual(OrderedList, Controller.Flash["items"]);
			Assert.AreEqual(newOrder, Controller.Flash["items"]);
		}

		[Test]
		public virtual void CanSuccessfullyMoveBootomItemUpOnePlace()
		{
			var newOrder = new List<TDto>(OrderedList);
			Controller.Flash["items"] = OrderedList;

			newOrder[2] = OrderedList[3];
			newOrder[3] = OrderedList[2];

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(Service.MoveItemUp(OrderedList[3].Id, OrderedList)).Return(newOrder))
				.Verify(() => Controller.MoveItemUp(OrderedList[3].Id));

			Assert.AreNotEqual(OrderedList, Controller.Flash["items"]);
			Assert.AreEqual(newOrder, Controller.Flash["items"]);
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
				.Expecting(() => Expect.Call(Service.SaveItemsOrder(OrderedList)).Return(success))
				.Verify(() => Controller.SaveOrder());
		}
	}
}