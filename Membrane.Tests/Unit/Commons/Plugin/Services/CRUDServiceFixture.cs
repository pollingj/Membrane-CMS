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
		protected IRepository<TEntity> Repository;
		protected ICRUDService<TDto, TEntity> Service;

		public List<TEntity> ListEntity { get; set; }
		public TEntity SingleEntity { private get; set; }
		public TDto SingleDTO { private get; set; }
		public List<TDto> ListDTO { get; set; }

		public override void SetUp()
		{
			base.SetUp();

			Repository = mockery.DynamicMock<IRepository<TEntity>>();
			Service = new CRUDService<TDto, TEntity>(Repository);
		}

		[Test]
		public virtual void CanMapSuccessfullyBetweenDTOAndEntity()
		{
			Service.RegisterMappings();
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
				.Expecting(() => Expect.Call(Repository.Find(new PagedItems<TEntity>(skip, take))).IgnoreArguments().Return(pagedEntities))
				.Verify(() => result = Service.GetPagedItems(currentPage, pageSize));

			Assert.AreEqual(pageSize, result.Count);
		}

		[Test]
		public virtual void CanGetUserGroupWithId()
		{
			var result = default(TDto);
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(Repository.FindById(SingleEntity.Id)).Return(SingleEntity))
				.Verify(() => result = Service.GetItem(SingleEntity.Id));

			Assert.AreEqual(SingleEntity.Id, result.Id);
		}

		[Test]
		public virtual void CanSuccessfullyEntity()
		{
			var result = Guid.Empty;
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => Repository.Save(SingleEntity)).IgnoreArguments())
				.Verify(() => result = Service.Create(SingleDTO));

			Assert.AreNotEqual(Guid.Empty, result);
		}

		[Test]
		public virtual void CanFailSaveEntity()
		{
			var result = Guid.Empty;
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => Repository.Save(SingleEntity)).IgnoreArguments().Throw(new RepositorySaveException()))
				.Verify(() => result = Service.Create(SingleDTO));

			Assert.AreEqual(Guid.Empty, result);
		}

		[Test]
		public virtual void CanSuccessfullyUpdateItem()
		{
			var result = false;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => Repository.Update(SingleEntity)).IgnoreArguments())
				.Verify(() => result = Service.Update(SingleDTO));

			Assert.IsTrue(result);
		}

		[Test]
		public virtual void CanFailUpdateItem()
		{
			var result = false;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => Repository.Update(SingleEntity)).IgnoreArguments().Throw(new RepositoryUpdateException()))
				.Verify(() => result = Service.Update(SingleDTO));

			Assert.IsFalse(result);
		}

		[Test]
		public virtual void CanSuccessfullyDeleteEntity()
		{
			var id = Guid.NewGuid();

			var result = false;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => Repository.Delete(id)))
				.Verify(() => result = Service.Delete(id));

			Assert.IsTrue(result);
		}

		[Test]
		public virtual void CanFailDeletingEntity()
		{
			var id = Guid.NewGuid();

			var result = false;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => Repository.Delete(id)).Throw(new RepositoryDeleteException()))
				.Verify(() => result = Service.Delete(id));

			Assert.IsFalse(result);
		}
	}
}