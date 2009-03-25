using System;
using Membrane.Controllers;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities.Enums;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers
{
	[TestFixture]
	public class LoginControllerFixture : BaseControllerFixture
	{
		private IAuthenticationService service;
		private LoginController controller;

		private readonly AuthenticationRequestDTO authenticationRequest = new AuthenticationRequestDTO {Username = "username", Password = "password"};

		public override void SetUp()
		{
			base.SetUp();

			service = mockery.DynamicMock<IAuthenticationService>();
			controller = new LoginController(service);

			PrepareController(controller);
		}

		[Test]
		public void ShouldAlertUserForInvalidLogin()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.AuthenticateUser(authenticationRequest)).Return(null))
				.Verify(() => controller.Login(authenticationRequest));

			Assert.AreEqual(1, controller.Flash.Count, "Flash error is not being populate");
			Assert.AreEqual("Username or Password not recognised", controller.Flash["error"], "Flash is not being populated with correct message");
		}

		[Test]
		public void SuccessfulAdministratorLoginShouldRedirectToAdminArea()
		{
			DoSuccessFullLogin(new AuthenticatedUserDTO { Id = Guid.NewGuid(), Type = UserType.Administrator }, UserType.Administrator, "/Administrator/Home/Index.castle");
		}

		[Test]
		public void SuccessfulUserLoginShouldRedirectToUserArea()
		{
			DoSuccessFullLogin(new AuthenticatedUserDTO { Id = Guid.NewGuid(), Type = UserType.User }, UserType.User, "/User/Home/Index.castle");
		}

		private void DoSuccessFullLogin(AuthenticatedUserDTO authenticatedUser, UserType role, string redirectPath)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.AuthenticateUser(authenticationRequest)).Return(authenticatedUser))
				.Verify(() => controller.Login(authenticationRequest));

			Assert.IsTrue(Context.CurrentUser.Identity.IsAuthenticated);
			Assert.IsTrue(Context.CurrentUser.IsInRole(role.ToString()));
			Assert.AreEqual(redirectPath, Response.RedirectedTo);
		}
	}
}