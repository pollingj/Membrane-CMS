using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.InMemory;
using Membrane.Core.Queries.Culture;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Core.Queries.Culture
{
	[TestFixture]
	public class DefaultCultureFixture
	{
		[Test]
		public void Should_return_matches_from_a_repository()
		{
			var users = new List<Membrane.Commons.Plugin.Entities.Culture>
                               {
									new Membrane.Commons.Plugin.Entities.Culture { Id = Guid.NewGuid(), Language = "English", ShortCode = "EN", IsDefault = true},
									new Membrane.Commons.Plugin.Entities.Culture { Id = Guid.NewGuid(), Language = "French", ShortCode = "FR", IsDefault = false},
									new Membrane.Commons.Plugin.Entities.Culture { Id = Guid.NewGuid(), Language = "Spanish", ShortCode = "ES", IsDefault = false}
                               };

			IRepository<Membrane.Commons.Plugin.Entities.Culture> repository = new InMemoryRepository<Membrane.Commons.Plugin.Entities.Culture>(users);
			var match = repository.FindOne(new DefaultCulture());

			Assert.AreEqual("English", match.Language);
			Assert.AreEqual("EN", match.ShortCode);
			Assert.IsTrue(match.IsDefault);
		}
	}
}