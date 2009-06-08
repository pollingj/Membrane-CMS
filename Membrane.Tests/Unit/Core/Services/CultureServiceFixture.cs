using System;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.Entities;
using Membrane.Core.Queries.Culture;
using Membrane.Core.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Core.Services
{
	[TestFixture]
	public class CultureServiceFixture : BaseFixture
	{
		private IRepository<Culture> repository;
		private CultureService service;

		public override void SetUp()
		{
			base.SetUp();

			repository = mockery.DynamicMock<IRepository<Culture>>();

			service = new CultureService(repository);
		}
		
		[Test]
		public void CanSuccessfullyGetDefaultCulture()
		{
			CultureDTO result = null;
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(repository.FindOne(new DefaultCulture())).IgnoreArguments().Return(new Culture { Id = Guid.NewGuid(), Language = "English", ShortCode = "EN", IsDefault = true }))
				.Verify(() => result = service.GetDefaultCulture());

			Assert.AreEqual("English", result.Name);
		}
	}
}