using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin;
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

		public override void SetUp()
		{
			base.SetUp();

			service = new OrderCRUDService<TDto, TEntity>(Repository);
		}

		[Test]
		public virtual void CanMoveItemDownOnePlace()
		{
			var newOrderList = new List<TDto>(ListDTO);
			newOrderList[0] = ListDTO[1];
			newOrderList[1] = ListDTO[0];

			var result = service.MoveItemDown(ListDTO[0].Id, ListDTO);

			Assert.AreNotEqual(ListDTO[0], result[0]);
			Assert.AreNotEqual(ListDTO[1], result[1]);
			Assert.AreEqual(newOrderList.Count, result.Count);

			for (var count = 0; count < result.Count; count++)
			{
				Assert.AreEqual(newOrderList[count], result[count]);
			}
		}

		[Test]
		public virtual void CanMoveItemUpOnePlace()
		{
			var newOrderList = new List<TDto>(ListDTO);
			newOrderList[5] = ListDTO[4];
			newOrderList[4] = ListDTO[5];

			var result = service.MoveItemUp(ListDTO[5].Id, ListDTO);

			Assert.AreNotEqual(ListDTO[4], result[4]);
			Assert.AreNotEqual(ListDTO[5], result[5]);
			Assert.AreEqual(newOrderList.Count, result.Count);

			for (var count = 0; count < result.Count; count++)
			{
				Assert.AreEqual(newOrderList[count], result[count]);
			}
		}

		[Test]
		public virtual void CanSuccessfullySaveOrderedList()
		{
			var result = false;
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => Repository.Update(ListEntity[0])).IgnoreArguments().Repeat.Times(ListEntity.Count))
				.Verify(() => result = service.SaveItemsOrder(ListDTO));

			Assert.IsTrue(result);
		}
	}
}