using System;
using Membrane.Controllers.User;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities.Enums;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.User
{
	[TestFixture]
	public class UserDetailsControllerFixture : BaseControllerFixture
	{
		private IUserService service;
		private UserDetailsController controller;

		public override void  TestFixtureSetUp()
		{
 			base.TestFixtureSetUp();

			service = mockery.DynamicMock<IUserService>();

			controller = new UserDetailsController(service);
			PrepareController(controller, "UserDetails");
		}


		[Test]
		public void UserCanViewTheirCurrentDetails()
		{
			var userDetails = new UserDetailsResponseDTO { Id = Guid.NewGuid(), Name = "John Polling", Email = "john@theusualsuspect.com"  };
			var authenticatedUser = new AuthenticatedUserDTO
			                            	{Id = Guid.NewGuid(), Type = UserType.Administrator};

			Context.Session["user"] = authenticatedUser;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.LoadDetails(authenticatedUser.Id)).Return(userDetails))
				.Verify(() => controller.Show());

			Assert.AreSame(userDetails, controller.PropertyBag["details"]);
			Assert.AreEqual(@"UserDetails\Action", controller.SelectedViewName);
		}

		[Test]
		public void UserCanSuccessfullyUpdateTheirCurrentDetails()
		{
			MockUserDetailsUpdate(true);

			Assert.IsNull(controller.Flash["errors"]);
			Assert.AreEqual("/UserDetails/Show.castle", Response.RedirectedTo);
		}

		[Test]
		public void UpdateDetailsCanFailValidation()
		{
			var updatedUserDetails = new UserDetailsRequestDTO
			                         	{
			                         		Id = Guid.NewGuid(),
			                         		Name = "John Polling",
			                         		Email = string.Empty,
			                         		Password = string.Empty,
			                         		ConfirmPassword = string.Empty
			                         	};

			controller.Update(updatedUserDetails);

			Assert.IsNotNull(controller.Flash["errors"]);
			Assert.AreEqual("/UserDetails/Show.castle", Response.RedirectedTo);
		}

		[Test]
		public void ErrorMessageCanBeShownOnFailureOfService()
		{
			MockUserDetailsUpdate(false);

			Assert.IsNotNull(controller.Flash["errors"]);
			Assert.AreEqual("/UserDetails/Show.castle", Response.RedirectedTo);
		}

		private void MockUserDetailsUpdate(bool success)
		{
			var updatedUserDetails = new UserDetailsRequestDTO
			{
				Id = Guid.NewGuid(),
				Name = "John Polling",
				Email = "john@theusualsusepct.com",
				Password = string.Empty,
				ConfirmPassword = string.Empty
			};
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.UpdateDetails(updatedUserDetails)).Return(success))
				.Verify(() => controller.Update(updatedUserDetails));
		}

	}
}