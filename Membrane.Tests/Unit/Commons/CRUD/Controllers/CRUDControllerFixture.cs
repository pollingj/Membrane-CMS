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

		public IList<DTO> listDTO { private get; set; }
		public DTO newDTO { private get; set; }
		public DTO invalidDTO { private get; set; }
		public DTO editDTO { private get; set; }
		public DTO deleteDTO { private get; set; }

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			service = mockery.DynamicMock<ICRUDService<DTO, Entity>>();

			controller = new CRUDController<DTO, Entity>(service);

			PrepareController(controller);

		}

		[Test]
		public void CanListUserGroupsWithNoPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedItems(defaultCurrentPageNumber, defaultCurrentPageSize)).Return(listDTO))
				.Verify(() => controller.List());

			AssertListData();
		}


		[Test]
		public void CanListUserGroupsWithPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedItems(anotherPageNumber, anotherPageSize)).Return(listDTO))
				.Verify(() => controller.List(anotherPageNumber, anotherPageSize));

			AssertListData();
		}

		[Test]
		public void CanShowNewUserGroupPage()
		{
			controller.New();

			Assert.AreEqual(typeof(DTO), controller.PropertyBag["itemtype"]);
			Assert.AreEqual(@"Controller\Form", controller.SelectedViewName);
		}

		[Test]
		public void CanSuccessfullySubmitNewUserGroup()
		{
			NewDTOSubmission(Guid.NewGuid());

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public void CanFailValidationOnSubmit()
		{
			controller.Submit(invalidDTO);

			AssertSubmitFailure(invalidDTO);
		}

		[Test]
		public void CanFailSubmitUserGroup()
		{
			NewDTOSubmission(Guid.Empty);

			AssertSubmitFailure(newDTO);
		}

		[Test]
		public void CanShowEditDTOPage()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetItem(editDTO.Id)).Return(editDTO))
				.Verify(() => controller.Edit(editDTO.Id));

			Assert.AreEqual(editDTO, controller.PropertyBag["item"]);
			Assert.AreEqual(@"Controller\Form", controller.SelectedViewName);
		}

		[Test]
		public void CanSuccessfullySubmitEdittedUserGroup()
		{
			EditDTOSubmission(true);

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public void CanFailSubmitEdittedUserGroup()
		{
			EditDTOSubmission(false);

			AssertSubmitFailure(editDTO);
		}

		[Test]
		public void CanShowUserGroupForConfirmDelete()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetItem(deleteDTO.Id)).Return(deleteDTO))
				.Verify(() => controller.ConfirmDelete(deleteDTO.Id));

			Assert.AreEqual(deleteDTO, controller.PropertyBag["item"]);
			Assert.AreEqual(@"Controller\Action", controller.SelectedViewName);
		}

		[Test]
		public void CanSuccessfullyDeleteUserGroup()
		{
			MockDeleteUserGroup(true);

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public void CanFailDeletingUserGroup()
		{
			MockDeleteUserGroup(false);

			Assert.IsNotNull(controller.Flash["error"]);
		}

		private void MockDeleteUserGroup(bool deleteSuccess)
		{
			var groupId = Guid.NewGuid();

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Delete(groupId)).Return(deleteSuccess))
				.Verify(() => controller.Delete(groupId));
		}

		private void NewDTOSubmission(Guid id)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Create(newDTO)).Return(id))
				.Verify(() => controller.Submit(newDTO));
		}

		private void EditDTOSubmission(bool updateSuccess)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Update(editDTO)).Return(updateSuccess))
				.Verify(() => controller.Submit(editDTO));
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
			Assert.AreEqual(listDTO, controller.PropertyBag["items"], "groups PropertyBag not being set");
		}
	}
}