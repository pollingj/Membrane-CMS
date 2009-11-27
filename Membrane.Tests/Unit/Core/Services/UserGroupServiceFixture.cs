using System;
using System.Collections.Generic;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Services;

namespace Membrane.Tests.Unit.Core.Services
{
	public class UserGroupServiceFixture : CRUDServiceFixture<UserGroupDTO, UserGroup>
	{
		public override void SetUp()
		{
			SingleEntity = new UserGroup { Id = Guid.NewGuid(), Name = "New Group"};
			ListEntity = new List<UserGroup>
			             	{
									new UserGroup {Id = Guid.NewGuid(), Name = "First Group"},
									new UserGroup {Id = Guid.NewGuid(), Name = "Second Group"},
									new UserGroup {Id = Guid.NewGuid(), Name = "Third Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "News Editor Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "Publishing Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "Blogging Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "Product Editing Group"}
			             	};
			SingleDTO = new UserGroupDTO {Id = Guid.NewGuid(), Name = "New Group"};
			base.SetUp();
		}
	}
}