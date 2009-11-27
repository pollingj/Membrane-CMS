using System;
using System.Collections.Generic;
using Membrane.Controllers.Administrator;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Controllers;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.Administrator
{
	public class UserGroupsControllerFixture : CRUDControllerFixture<UserGroupDTO, UserGroup>
	{
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();
			NewDTO = new UserGroupDTO {Name = "New Group"};
			EditDTO = new UserGroupDTO {Id = Guid.NewGuid(), Name = "Edit Group"};
			InvalidDTO = new UserGroupDTO {Id = Guid.NewGuid()};
			DeleteDTO = new UserGroupDTO {Id = Guid.NewGuid(), Name = "Delete Group"};

			ListDTO = new List<UserGroupDTO>
			          	{
			          		new UserGroupDTO {Id = Guid.NewGuid(), Name = "Blogging Group"},
			          		new UserGroupDTO {Id = Guid.NewGuid(), Name = "News Group"},
			          		new UserGroupDTO {Id = Guid.NewGuid(), Name = "Exhibitions Group"},
			          		new UserGroupDTO {Id = Guid.NewGuid(), Name = "Full Access Group"}
			          	};


			Controller = new UserGroupsController(Service, PropertyReaderService);

			PrepareController(Controller);
		}
	}
}