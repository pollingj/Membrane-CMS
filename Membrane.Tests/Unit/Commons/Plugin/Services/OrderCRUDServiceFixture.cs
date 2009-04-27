using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Commons.Plugin.Services
{
	public class OrderCRUDServiceFixture<TDto, TEntity> : CRUDServiceFixture<TDto, TEntity>
		where TDto : IOrderedDto
		where TEntity : IOrderedEntity
	{
		private IOrderCRUDService<TDto, TEntity> service;
		public ItemOrderRequestDTO CurrentItemOrder;

		public override void SetUp()
		{
			base.SetUp();

			service = new OrderCRUDService<TDto, TEntity>(Repository);
		}

		[Test]
		public virtual void CanMoveItemDownOnePlace()
		{
			var newOrderList = new List<Guid>(CurrentItemOrder.Ids);
			newOrderList[0] = CurrentItemOrder.Ids[1];
			newOrderList[1] = CurrentItemOrder.Ids[0];

			var result = service.MoveItemDown(CurrentItemOrder, CurrentItemOrder.Ids[0]);

			Assert.AreNotEqual(CurrentItemOrder.Ids[0], result.Ids[0]);
			Assert.AreNotEqual(CurrentItemOrder.Ids[1], result.Ids[1]);
			Assert.AreEqual(newOrderList.Count, result.Ids.Length);

			for (var count = 0; count < result.Ids.Length; count++)
			{
				Assert.AreEqual(newOrderList[count], result.Ids[count]);
			}
		}

		[Test]
		public virtual void CanMoveItemUpOnePlace()
		{
			var newOrderList = new List<Guid>(CurrentItemOrder.Ids);
			newOrderList[5] = CurrentItemOrder.Ids[4];
			newOrderList[4] = CurrentItemOrder.Ids[5];

			var result = service.MoveItemUp(CurrentItemOrder, CurrentItemOrder.Ids[5]);

			Assert.AreNotEqual(CurrentItemOrder.Ids[4], result.Ids[4]);
			Assert.AreNotEqual(CurrentItemOrder.Ids[5], result.Ids[5]);
			Assert.AreEqual(newOrderList.Count, result.Ids.Length);

			for (var count = 0; count < result.Ids.Length; count++)
			{
				Assert.AreEqual(newOrderList[count], result.Ids[count]);
			}
		}

		[Test]
		public virtual void CanSuccessfullySaveOrderedList()
		{
			var result = false;
			With.Mocks(mockery)
				.Expecting(() =>
				           	{
								Expect.Call(Repository.FindById(Guid.NewGuid())).IgnoreArguments().Repeat.Times(ListEntity.Count).Return(SingleEntity);
				           		Expect.Call(() => Repository.Update(ListEntity[0])).IgnoreArguments().Repeat.Times(ListEntity.Count);
				           	})
				.Verify(() => result = service.SaveItemsOrder(CurrentItemOrder));

			Assert.IsTrue(result);
		}
	}
}