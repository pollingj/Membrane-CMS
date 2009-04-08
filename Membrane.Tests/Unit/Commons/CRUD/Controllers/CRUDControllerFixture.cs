using System;
using System.Collections.Generic;
using Membrane.Commons.CRUD;
using Membrane.Commons.CRUD.Controllers;
using Membrane.Commons.CRUD.Services;
using Membrane.Commons.Persistence;
using Membrane.Tests.Unit.Web.MonoRail.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Commons.CRUD.Controllers
{
	[TestFixture]
	public class CRUDControllerFixture<DTO, Entity> : BaseControllerFixture
		where DTO : IDTO
		where Entity : IEntity
	{
		private CRUDController<DTO, Entity> controller;
		private ICRUDService<DTO, Entity> service;

		private const int defaultCurrentPageNumber = 1;
		private const int defaultCurrentPageSize = 10;
		private const int anotherPageNumber = 2;
		private const int anotherPageSize = 20;

		public IList<DTO> ListDTO { private get; set; }
		public DTO NewDTO { private get; set; }
		public DTO InvalidDTO { private get; set; }
		public DTO EditDTO { private get; set; }
		public DTO DeleteDTO { private get; set; }

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			service = mockery.DynamicMock<ICRUDService<DTO, Entity>>();

			controller = new CRUDController<DTO, Entity>(service);

			PrepareController(controller);

		}

		[Test]
		public virtual void CanListItemsWithNoPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedItems(defaultCurrentPageNumber, defaultCurrentPageSize)).Return(ListDTO))
				.Verify(() => controller.List());

			AssertListData();
		}


		[Test]
		public virtual void CanListItemsWithPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedItems(anotherPageNumber, anotherPageSize)).Return(ListDTO))
				.Verify(() => controller.List(anotherPageNumber, anotherPageSize));

			AssertListData();
		}

		[Test]
		public virtual void CanShowNewItemPage()
		{
			controller.New();

			Assert.AreEqual(typeof(DTO), controller.PropertyBag["itemtype"]);
			Assert.AreEqual(@"Controller\Form", controller.SelectedViewName);
		}

		[Test]
		public virtual void CanSuccessfullySubmitNewItem()
		{
			NewDTOSubmission(Guid.NewGuid());

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public virtual void CanFailValidationOnSubmit()
		{
			controller.Submit(InvalidDTO);

			AssertSubmitFailure(InvalidDTO);
		}

		[Test]
		public virtual void CanFailSubmitItem()
		{
			NewDTOSubmission(Guid.Empty);

			AssertSubmitFailure(NewDTO);
		}

		[Test]
		public virtual void CanShowEditItemPage()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetItem(EditDTO.Id)).Return(EditDTO))
				.Verify(() => controller.Edit(EditDTO.Id));

			Assert.AreEqual(EditDTO, controller.PropertyBag["item"]);
			Assert.AreEqual(@"Controller\Form", controller.SelectedViewName);
		}

		[Test]
		public virtual void CanSuccessfullySubmitEdittedItem()
		{
			EditDTOSubmission(true);

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public virtual void CanFailSubmitEdittedItem()
		{
			EditDTOSubmission(false);

			AssertSubmitFailure(EditDTO);
		}

		[Test]
		public virtual void CanShowItemForConfirmDelete()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetItem(DeleteDTO.Id)).Return(DeleteDTO))
				.Verify(() => controller.ConfirmDelete(DeleteDTO.Id));

			Assert.AreEqual(DeleteDTO, controller.PropertyBag["item"]);
			Assert.AreEqual(@"Controller\Action", controller.SelectedViewName);
		}

		[Test]
		public virtual void CanSuccessfullyDeleteItem()
		{
			MockDeleteItem(true);

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public virtual void CanFailDeletingItem()
		{
			MockDeleteItem(false);

			Assert.IsNotNull(controller.Flash["error"]);
		}

		private void MockDeleteItem(bool deleteSuccess)
		{
			var groupId = Guid.NewGuid();

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Delete(groupId)).Return(deleteSuccess))
				.Verify(() => controller.Delete(groupId));
		}

		private void NewDTOSubmission(Guid id)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Create(NewDTO)).Return(id))
				.Verify(() => controller.Submit(NewDTO));
		}

		private void EditDTOSubmission(bool updateSuccess)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Update(EditDTO)).Return(updateSuccess))
				.Verify(() => controller.Submit(EditDTO));
		}

		private void AssertSuccessfulActionAndRedirectedBackToList()
		{
			Assert.IsNull(controller.Flash["error"]);
			Assert.AreEqual("/Controller/List.castle", Response.RedirectedTo);
		}

		private void AssertSubmitFailure(DTO failedDTO)
		{
			Assert.AreEqual(failedDTO, controller.Flash["item"]);
			Assert.IsNotNull(controller.Flash["error"]);
			Assert.AreEqual(@"Controller\Action", controller.SelectedViewName);
		}


		private void AssertListData()
		{
			Assert.AreEqual(@"Controller\Action", controller.SelectedViewName, "List view not being used");
			Assert.AreEqual(ListDTO, controller.PropertyBag["items"], "groups PropertyBag not being set");
		}
	}
}