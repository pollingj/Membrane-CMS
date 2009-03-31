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

			PrepareController(controller, "Administrator", "UserGroups", "List");

			userGroupList = new List<UserGroupDTO>
			               	{
			               		new UserGroupDTO { Id = Guid.NewGuid(), Name = "News Editors" },
								new UserGroupDTO { Id = Guid.NewGuid(), Name = "Navigation Editors" },
								new UserGroupDTO { Id = Guid.NewGuid(), Name = "Jobs Editors" }
			               	};
		}

		[Test]
		public void CanListUserTypesWithNoPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedUserGroups(defaultCurrentPageNumber, defaultCurrentPageSize)).Return(userGroupList))
				.Verify(() => controller.List());

			AssertListData();
		}


		[Test]
		public void CanListUserTypesWithPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedUserGroups(anotherPageNumber, anotherPageSize)).Return(userGroupList))
				.Verify(() => controller.List(anotherPageNumber, anotherPageSize));

			AssertListData();
		}

		private void AssertListData()
		{
			Assert.AreEqual(@"Administrator\UserGroups\List", controller.SelectedViewName, "List view not being used");
			Assert.AreEqual(userGroupList, controller.PropertyBag["groups"], "groups PropertyBag not being set");
		}
	}
}