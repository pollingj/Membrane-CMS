using System;
using System.Collections.Generic;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.Services;
using Membrane.Controllers.User;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.User
{
	/// <summary>
	/// Test fixture for the <see cref="NavigationController"/>
	/// </summary>
	public class NavigationControllerFixture : OrderCRUDControllerFixture<NavigationNodeDTO, NavigationNode>
	{
		private ICRUDService<NavigationTypeDTO, NavigationType> navTypeService;



		/// <summary>
		/// Overriding the base SetUp to set up the relevant TDto objects
		/// </summary>
		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();
			var type = new NavigationTypeDTO {Id = Guid.NewGuid(), Name = "Primary Navigation"};
			NewDTO = new NavigationNodeDTO {Name = "Home", AccessKey = '1', Title = "Visit our home page", Type = type};
			EditDTO = new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "About Us", AccessKey = '2', Title = "More info about us", Type = type};
			InvalidDTO = new NavigationNodeDTO {Id = Guid.NewGuid()};
			DeleteDTO = new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "Contact Us", AccessKey = '3', Title = "Feel free to contact us", Type = type};
			ListDTO = new List<NavigationNodeDTO>
			{
				new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "Home"},
				new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "About Us"},
				new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "News"},
				new NavigationNodeDTO {Id = Guid.NewGuid(), Name = "Contact Us"}
			};


			OrderedList = new ItemOrderRequestDTO
			              	{
			              		Ids = new[]
			              		      	{
			              		      		Guid.NewGuid(),
			              		      		Guid.NewGuid(),
			              		      		Guid.NewGuid(),
			              		      		Guid.NewGuid()
			              		      	}
			              	};

			navTypeService = mockery.DynamicMock<ICRUDService<NavigationTypeDTO, NavigationType>>();

			Controller = new NavigationController(OrderedService, PropertyReaderService, navTypeService);

			PrepareController(Controller);
		}

		[Test]
		public void CanLoadSupportiveDataSuccessfully()
		{
			var navTypes = new List<NavigationTypeDTO>
			               	{
			               		new NavigationTypeDTO {Id = Guid.NewGuid(), Name = "Primary Navigation"},
			               		new NavigationTypeDTO {Id = Guid.NewGuid(), Name = "Secondary Navigation"},
			               		new NavigationTypeDTO {Id = Guid.NewGuid(), Name = "Tertiary Navigation"}
			               	};

			With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(navTypeService.GetItems()).Return(navTypes);
								Expect.Call(Service.GetItems()).Return(ListDTO);
				           	})
				.Verify(() => Controller.LoadSupportiveData());

			Assert.AreEqual(navTypes, Controller.PropertyBag["support.NavigationTypeDTO"]);
			Assert.AreEqual(ListDTO, Controller.PropertyBag["support.NavigationNodeDTO"]);
		}
	}
}