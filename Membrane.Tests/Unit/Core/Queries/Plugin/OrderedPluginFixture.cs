using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.InMemory;
using Membrane.Core.Queries.Plugin;
using Membrane.Entities;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Core.Queries.Plugin
{
	[TestFixture]
	public class OrderedPluginFixture
	{
		[Test]
		public void Should_return_matches_from_a_repository()
		{
			var users = new List<InstalledPlugin>
                               {
									new InstalledPlugin { Id = Guid.NewGuid(), Name = "Blog", Version = "1.0.0"},
									new InstalledPlugin { Id = Guid.NewGuid(), Name = "News", Version = "1.0.0"},
									new InstalledPlugin { Id = Guid.NewGuid(), Name = "Events", Version = "1.0.0"}
                               };

			IRepository<InstalledPlugin> repository = new InMemoryRepository<InstalledPlugin>(users);
			var matches = repository.Find(new OrderedPlugins());

			Assert.AreEqual(3, matches.Count);

			var count = 0;
			foreach (var item in matches)
			{
				if (count == 0)
					Assert.AreEqual("Blog", item.Name);
				if (count == 1)
					Assert.AreEqual("Events", item.Name);
				if (count == 2)
					Assert.AreEqual("News", item.Name);

				count++;
			}
		}
	}
}