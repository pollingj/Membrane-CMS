using System;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.CRUD.Controllers;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.Administrator
{
	[TestFixture]
	public class UserGroupsControllerFixture : CRUDControllerFixture<UserGroupDTO, UserGroup>
	{
		[SetUp]
		public override void SetUp()
		{
			newDTO = new UserGroupDTO {Name = "New Group"};
			editDTO = new UserGroupDTO {Id = Guid.NewGuid(), Name = "Edit Group"};
			invalidDTO = new UserGroupDTO {Id = Guid.NewGuid()};
			deleteDTO = new UserGroupDTO {Id = Guid.NewGuid(), Name = "Delete Group"};
			base.SetUp();
		}
	}
}