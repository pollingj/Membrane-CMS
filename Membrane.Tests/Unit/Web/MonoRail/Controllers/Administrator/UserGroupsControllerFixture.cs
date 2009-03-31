using System;
using System.Collections.Generic;
using Membrane.Controllers.Administrator;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.Administrator
{
	[TestFixture]
	public class UserGroupsControllerFixture : BaseControllerFixture
	{
		private UserGroupsController controller;
		private IUserGroupService service;

		private const int defaultCurrentPageNumber = 1;
		private const int defaultCurrentPageSize = 10;
		private const int anotherPageNumber = 2;
		private const int anotherPageSize = 20;

		private IList<UserGroupDTO> userGroupList;

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			service = mockery.DynamicMock<IUserGroupService>();

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
				.Expecting(() => Expect.Call(service.GetPagedUserGroups(defaultCurrentPageNumber, defaultCurrentPageSize)).Return(userGroupList))
				.Verify(() => controller.List());

			AssertListData();
		}


		[Test]
		public void CanListUserGroupsWithPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedUserGroups(anotherPageNumber, anotherPageSize)).Return(userGroupList))
				.Verify(() => controller.List(anotherPageNumber, anotherPageSize));

			AssertListData();
		}

		[Test]
		public void CanShowNewUserGroupPage()
		{
			controller.New();

			Assert.AreEqual(typeof(UserGroupDTO), controller.PropertyBag["grouptype"]);
			Assert.AreEqual(@"UserGroups\Action", controller.SelectedViewName);
		}
		
		[Test]
		public void CanSuccessfullySubmitNewUserGroup()
		{
			var newGroup = new UserGroupDTO {Name = "New User Group"};
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Create(newGroup)).Return(Guid.NewGuid()))
				.Verify(() => controller.Submit(newGroup));

			Assert.IsNull(controller.Flash["error"]);
			Assert.AreEqual("/UserGroups/List.castle", Response.RedirectedTo);
		}

		[Test]
		public void CanFailSubmitNewUserGroup()
		{
			var newGroup = new UserGroupDTO { Name = "New User Group" };
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Create(newGroup)).Return(Guid.Empty))
				.Verify(() => controller.Submit(newGroup));

			Assert.AreEqual(newGroup, controller.Flash["group"]);
			Assert.IsNotNull(controller.Flash["error"]);
			Assert.AreEqual(@"UserGroups\Action", controller.SelectedViewName);
		}



		private void AssertListData()
		{
			Assert.AreEqual(@"UserGroups\Action", controller.SelectedViewName, "List view not being used");
			Assert.AreEqual(userGroupList, controller.PropertyBag["groups"], "groups PropertyBag not being set");
		}
	}
}