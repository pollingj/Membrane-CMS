using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.InMemory;
using Membrane.Core.Queries.Users;
using Membrane.Entities;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Core.Queries.Users
{
	[TestFixture]
	public class UserByUsernameAndPasswordFixture
	{
		[Test]
		public void Should_return_matches_from_a_repository()
		{
			var users = new List<MembraneUser>
                               {
                                   new MembraneUser {Username = "andypike", Password = "apass"},
                                   new MembraneUser {Username = "johnpolling", Password = "jpass"},
                                   new MembraneUser {Username = "someone", Password = "pass"},
                                   new MembraneUser {Username = "another", Password = "pass"},
                                   new MembraneUser {Username = "onemore", Password = "pass"}
                               };

			IRepository<MembraneUser> repository = new InMemoryRepository<MembraneUser>(users);
			var matches = repository.Find(new UserByUsernameAndPassword("johnpolling", "jpass"));

			Assert.AreEqual(1, matches.Count);
		}

	}
}