using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Core.DTOs;
using Membrane.Core.Queries.UserGroups;
using Membrane.Core.Services;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Core.Services
{
	[TestFixture]
	public class UserGroupServiceFixture : BaseFixture
	{
		private IUserGroupService service;
		private IRepository<UserGroup> userGroupRepository;

		public override void SetUp()
		{
			base.SetUp();

			userGroupRepository = mockery.DynamicMock<IRepository<UserGroup>>();
			service = new UserGroupService(userGroupRepository);
		}

		[Test]
		public void CanGetPagedUserGroups()
		{
			var currentPage = 2;
			var pageSize = 3;
			var skip = 3;
			var take = 3;

			var userGroups = new List<UserGroup>
			                 	{
									new UserGroup {Id = Guid.NewGuid(), Name = "First Group"},
									new UserGroup {Id = Guid.NewGuid(), Name = "Second Group"},
									new UserGroup {Id = Guid.NewGuid(), Name = "Third Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "News Editor Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "Publishing Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "Blogging Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "Product Editing Group"}
			                 	};

			List<UserGroup> pagedGroups = userGroups.GetRange(skip, take);

			ICollection<UserGroupDTO> result = new List<UserGroupDTO>();

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(userGroupRepository.Find(new PagedUserGroups(skip, take))).IgnoreArguments().Return(pagedGroups))
				.Verify(() => result = service.GetPagedUserGroups(currentPage, pageSize));

			Assert.AreEqual(pageSize, result.Count);

			var count = 0;
			foreach (var item in result)
			{
				if (count == 0)
					Assert.AreEqual("News Editor Group", item.Name);
				if (count == 1)
					Assert.AreEqual("Publishing Group", item.Name);
				if (count == 2)
					Assert.AreEqual("Blogging Group", item.Name);

				count++;
			}
		}
	}
}