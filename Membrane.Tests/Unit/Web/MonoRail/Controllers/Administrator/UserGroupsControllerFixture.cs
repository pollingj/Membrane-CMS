using System;
using System.Collections.Generic;
using Membrane.Commons.CRUD;
using Membrane.Controllers.Administrator;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.Administrator
{
	[TestFixture]
	public class UserGroupsControllerFixture : BaseControllerFixture
	{
		private UserGroupsController controller;
		private ICRUDService<UserGroupDTO, UserGroup> service;
		//private IUserGroupService service;

		private const int defaultCurrentPageNumber = 1;
		private const int defaultCurrentPageSize = 10;
		private const int anotherPageNumber = 2;
		private const int anotherPageSize = 20;

		private IList<UserGroupDTO> userGroupList;

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			service = mockery.DynamicMock<ICRUDService<UserGroupDTO, UserGroup>>();

			controller = new UserGroupsController(service);

			PrepareController(controller, "UserGroups");

			userGroupList = new List<UserGroupDTO>
			               	{
			               		new UserGroupDTO { Id = Guid.NewGuid(), Name = "News Editors" },
								new UserGroupDTO { Id = Guid.NewGuid(), Name = "Navigation Editors" },
								new UserGroupDTO { Id = Guid.NewGuid(), Name = "Jobs Editors" }
			               	};
		}

		[Test]
		public void CanListUserGroupsWithNoPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedItems(defaultCurrentPageNumber, defaultCurrentPageSize)).Return(userGroupList))
				.Verify(() => controller.List());

			AssertListData();
		}


		[Test]
		public void CanListUserGroupsWithPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedItems(anotherPageNumber, anotherPageSize)).Return(userGroupList))
				.Verify(() => controller.List(anotherPageNumber, anotherPageSize));

			AssertListData();
		}

		[Test]
		public void CanShowNewUserGroupPage()
		{
			controller.New();

			Assert.AreEqual(typeof(UserGroupDTO), controller.PropertyBag["itemtype"]);
			Assert.AreEqual(@"UserGroups\Form", controller.SelectedViewName);
		}
		
		[Test]
		public void CanSuccessfullySubmitNewUserGroup()
		{
			var newGroup = new UserGroupDTO {Name = "New User Group"};
			NewUserGroupSubmission(newGroup, Guid.NewGuid());

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public void CanFailValidationOnSubmit()
		{
			var newGroup = new UserGroupDTO {Name = ""};

			controller.Submit(newGroup);

			AssertSubmitFailure(newGroup);
		}

		[Test]
		public void CanFailSubmitUserGroup()
		{
			var newGroup = new UserGroupDTO { Name = "New User Group" };

			NewUserGroupSubmission(newGroup, Guid.Empty);

			AssertSubmitFailure(newGroup);
		}

		[Test]
		public void CanShowEditUserGroupPage()
		{
			var groupId = Guid.NewGuid();
			var group = new UserGroupDTO {Id = groupId, Name = "Stored User Group"};
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetItem(groupId)).Return(group))
				.Verify(() => controller.Edit(groupId));

			Assert.AreEqual(group, controller.PropertyBag["item"]);
			Assert.AreEqual(@"UserGroups\Form", controller.SelectedViewName);	
		}

		[Test]
		public void CanSuccessfullySubmitEdittedUserGroup()
		{
			var edittedId = Guid.NewGuid();
			var edittedGroup = new UserGroupDTO {Id = edittedId, Name = "Editted Group"};

			EditUserGroupSubmission(edittedGroup, true);

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public void CanFailSubmitEdittedUserGroup()
		{
			var edittedId = Guid.NewGuid();
			var edittedGroup = new UserGroupDTO { Id = edittedId, Name = "Editted Group" };

			EditUserGroupSubmission(edittedGroup, false);

			AssertSubmitFailure(edittedGroup);
		}

		[Test]
		public void CanShowUserGroupForConfirmDelete()
		{
			var groupId = Guid.NewGuid();
			var deleteGroup = new UserGroupDTO {Id = Guid.NewGuid(), Name = "Delete User Group"};
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetItem(groupId)).Return(deleteGroup))
				.Verify(() => controller.ConfirmDelete(groupId));

			Assert.AreEqual(deleteGroup, controller.PropertyBag["item"]);
			Assert.AreEqual(@"UserGroups\Action", controller.SelectedViewName);
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

		private void NewUserGroupSubmission(UserGroupDTO userGroup, Guid id)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Create(userGroup)).Return(id))
				.Verify(() => controller.Submit(userGroup));
		}

		private void EditUserGroupSubmission(UserGroupDTO userGroup, bool updateSuccess)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Update(userGroup)).Return(updateSuccess))
				.Verify(() => controller.Submit(userGroup));
		}

		private void AssertSuccessfulActionAndRedirectedBackToList()
		{
			Assert.IsNull(controller.Flash["error"]);
			Assert.AreEqual("/UserGroups/List.castle", Response.RedirectedTo);
		}

		private void AssertSubmitFailure(UserGroupDTO group)
		{
			Assert.AreEqual(group, controller.Flash["item"]);
			Assert.IsNotNull(controller.Flash["error"]);
			Assert.AreEqual(@"UserGroups\Action", controller.SelectedViewName);			
		}


		private void AssertListData()
		{
			Assert.AreEqual(@"UserGroups\Action", controller.SelectedViewName, "List view not being used");
			Assert.AreEqual(userGroupList, controller.PropertyBag["items"], "groups PropertyBag not being set");
		}
	}
}