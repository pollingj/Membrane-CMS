using System;
using Membrane.Controllers.Administrator;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Controllers;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.Administrator
{
	public class UserGroupsControllerFixture : CRUDControllerFixture<UserGroupDTO, UserGroup>
	{
		public override void SetUp()
		{
			base.SetUp();
			NewDTO = new UserGroupDTO {Name = "New Group"};
			EditDTO = new UserGroupDTO {Id = Guid.NewGuid(), Name = "Edit Group"};
			InvalidDTO = new UserGroupDTO {Id = Guid.NewGuid()};
			DeleteDTO = new UserGroupDTO {Id = Guid.NewGuid(), Name = "Delete Group"};
			

			Controller = new UserGroupsController(Service, AutoGenerator);

			PrepareController(Controller);
		}
	}
}