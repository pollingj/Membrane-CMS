using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.InMemory;
using Membrane.Core.Queries.Users;
using Membrane.Entities;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Membrane.Tests.Unit.Core.Queries.Users
{
	[TestFixture]
	public class UserByUsernameAndPasswordFixture
	{
		[Test]
		public void Should_return_matches_from_a_repository()
		{
			var users = new List<User>
                               {
                                   new User {Username = "andypike", Password = "apass"},
                                   new User {Username = "johnpolling", Password = "jpass"},
                                   new User {Username = "someone", Password = "pass"},
                                   new User {Username = "another", Password = "pass"},
                                   new User {Username = "onemore", Password = "pass"}
                               };

			IRepository<User> repository = new InMemoryRepository<User>(users);
			var matches = repository.Find(new UserByUsernameAndPassword("johnpolling", "jpass"));

			Assert.That(matches.Count, Is.EqualTo(1));
		}

	}
}