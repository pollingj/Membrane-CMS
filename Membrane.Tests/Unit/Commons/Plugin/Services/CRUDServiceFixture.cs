using System;
using System.Collections.Generic;
using AutoMapper;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.Exceptions;
using Membrane.Commons.Plugin;
using Membrane.Commons.Plugin.Queries;
using Membrane.Commons.Plugin.Services;
using Membrane.Tests.Unit.Core;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Commons.Plugin.Services
{
	[TestFixture]
	public class CRUDServiceFixture<TDto, TEntity> : BaseFixture
		where TDto : IDto
		where TEntity : IEntity
	{
		private IRepository<TEntity> repository;
		private ICRUDService<TDto, TEntity> service;

		public List<TEntity> ListEntity { get; set; }
		public TEntity SingleEntity { private get; set; }
		public TDto SingleDTO { private get; set; }
		public List<TDto> ListDTO { private get; set; }

		public override void SetUp()
		{
			base.SetUp();

			repository = mockery.DynamicMock<IRepository<TEntity>>();
			service = new CRUDService<TDto, TEntity>(repository);
		}

		[Test]
		public virtual void CanMapSuccessfullyBetweenDTOAndEntity()
		{
			service.RegisterMappings();
			Mapper.AssertConfigurationIsValid();
		}

		[Test]
		public virtual void CanGetPagedItems()
		{
			var currentPage = 2;
			var pageSize = 3;
			var skip = 3;
			var take = 3;


			var pagedEntities = ListEntity.GetRange(skip, take);

			ICollection<TDto> result = new List<TDto>();

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(repository.Find(new PagedItems<TEntity>(skip, take))).IgnoreArguments().Return(pagedEntities))
				.Verify(() => result = service.GetPagedItems(currentPage, pageSize));

			Assert.AreEqual(pageSize, result.Count);
		}

		[Test]
		public virtual void CanGetUserGroupWithId()
		{
			var result = default(TDto);
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(repository.FindById(SingleEntity.Id)).Return(SingleEntity))
				.Verify(() => result = service.GetItem(SingleEntity.Id));

			Assert.AreEqual(SingleEntity.Id, result.Id);
		}

		[Test]
		public virtual void CanSuccessfullySaveUserGroup()
		{
			var result = Guid.Empty;
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => repository.Save(SingleEntity)).IgnoreArguments())
				.Verify(() => result = service.Create(SingleDTO));

			Assert.AreNotEqual(Guid.Empty, result);
		}

		[Test]
		public virtual void CanFailSaveUserGroup()
		{
			var result = Guid.Empty;
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => repository.Save(SingleEntity)).IgnoreArguments().Throw(new RepositorySaveException()))
				.Verify(() => result = service.Create(SingleDTO));

			Assert.AreEqual(Guid.Empty, result);
		}

		[Test]
		public virtual void CanSuccessfullyUpdateItem()
		{
			var result = false;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => repository.Update(SingleEntity)).IgnoreArguments())
				.Verify(() => result = service.Update(SingleDTO));

			Assert.IsTrue(result);
		}

		[Test]
		public virtual void CanFailUpdateItem()
		{
			var result = false;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => repository.Update(SingleEntity)).IgnoreArguments().Throw(new RepositoryUpdateException()))
				.Verify(() => result = service.Update(SingleDTO));

			Assert.IsFalse(result);
		}

		[Test]
		public virtual void CanSuccessfullyDeleteUserGroup()
		{
			var id = Guid.NewGuid();

			var result = false;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => repository.Delete(id)))
				.Verify(() => result = service.Delete(id));

			Assert.IsTrue(result);
		}

		[Test]
		public virtual void CanFailDeletingUserGroup()
		{
			var id = Guid.NewGuid();

			var result = false;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => repository.Delete(id)).Throw(new RepositoryDeleteException()))
				.Verify(() => result = service.Delete(id));

			Assert.IsFalse(result);
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
	}
}