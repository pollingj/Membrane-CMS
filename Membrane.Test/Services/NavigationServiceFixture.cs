using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Core.DTOs;
using Membrane.Core.Services;
using Membrane.Entities;
using NUnit.Framework;
using Rhino.Mocks;
using With=Rhino.Mocks.With;

namespace Membrane.Test.Services
{
	/// <summary>
	/// 
	/// </summary>
	[TestFixture]
	public class NavigationServiceFixture
	{
		private IRepository<NavigationType> navigationTypeRepository;
		private NavigationService service;
		private MockRepository mockery;

		[SetUp]
		public void SetUp()
		{
			mockery = new MockRepository();
			navigationTypeRepository = mockery.DynamicMock<IRepository<NavigationType>>();

			service = new NavigationService(navigationTypeRepository);

		}

		[Test]
		public void CanGetAllNavigationTypes()
		{
			// Need to put this creation into a base unit test class so that other tests 
			// can use it when required
			var typeList = new List<NavigationType>
			               	{
			               		new NavigationType {Id = 1, Name = "Primary Navigation"},
			               		new NavigationType {Id = 2, Name = "Secondary Navigation"}
			               	};
			ICollection<NavigationTypeDTO> result;

			// User of Fluent Rhino Mocks syntax
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(navigationTypeRepository.Find()).Return(typeList))
				.Verify(
				() =>
					{
						result = service.GetAllNavigationTypes();
						Assert.AreEqual(2, result.Count);
					}
				);

			
		}
	}
}