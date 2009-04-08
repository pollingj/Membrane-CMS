using System;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.CRUD.Controllers;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.Administrator
{
	public class UserGroupsControllerFixture : CRUDControllerFixture<UserGroupDTO, UserGroup>
	{
		public override void SetUp()
		{
			NewDTO = new UserGroupDTO {Name = "New Group"};
			EditDTO = new UserGroupDTO {Id = Guid.NewGuid(), Name = "Edit Group"};
			InvalidDTO = new UserGroupDTO {Id = Guid.NewGuid()};
			DeleteDTO = new UserGroupDTO {Id = Guid.NewGuid(), Name = "Delete Group"};
			base.SetUp();
		}
	}
}