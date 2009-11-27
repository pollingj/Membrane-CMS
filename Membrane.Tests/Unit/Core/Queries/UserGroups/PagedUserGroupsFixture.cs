using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.InMemory;
using Membrane.Core.Queries.UserGroups;
using Membrane.Entities;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Core.Queries.UserGroups
{
	[TestFixture]
	public class PagedUserGroupsFixture
	{
		[Test]
		public void Should_return_matches_from_a_repository()
		{
			var users = new List<UserGroup>
                               {
									new UserGroup {Id = Guid.NewGuid(), Name = "First Group"},
									new UserGroup {Id = Guid.NewGuid(), Name = "Second Group"},
									new UserGroup {Id = Guid.NewGuid(), Name = "Third Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "News Editor Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "Publishing Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "Blogging Group"},
			                 		new UserGroup {Id = Guid.NewGuid(), Name = "Product Editing Group"}
                               };

			IRepository<UserGroup> repository = new InMemoryRepository<UserGroup>(users);
			var matches = repository.Find(new PagedUserGroups(3, 3));

			Assert.AreEqual(3, matches.Count);

			var count = 0;
			foreach (var item in matches)
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