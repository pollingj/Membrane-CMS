using System;
using Membrane.Controllers.Administrator;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.CRUD.Controllers;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.Administrator
{
	/// <summary>
	/// Test fixture for the <see cref="NavigationTypesController"/>
	/// </summary>
	public class NavigationTypesControllerFixture : CRUDControllerFixture<NavigationTypeDTO, NavigationType>
	{
		/// <summary>
		/// Overriding the base SetUp to set up the relevant TDto objects
		/// </summary>
		public override void SetUp()
		{
			base.SetUp();
			NewDTO = new NavigationTypeDTO { Name = "Primary Navigation" };
			EditDTO = new NavigationTypeDTO { Id = Guid.NewGuid(), Name = "Secondary Navigation" };
			InvalidDTO = new NavigationTypeDTO { Id = Guid.NewGuid() };
			DeleteDTO = new NavigationTypeDTO { Id = Guid.NewGuid(), Name = "Tertiary Navigation" };

			Controller = new NavigationTypesController(Service, AutoGenerator);

			PrepareController(Controller);

		}
	}
}