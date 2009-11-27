using System;
using System.Configuration;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Commons.Wrappers.Interfaces;
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
		private IAuthenticationService authenticationService;
		private IPluginsService pluginsService;
		private ICultureService cultureService;
		private IFormsAuthentication formsAuthentication;
		private IServiceProvider serviceProvider;
		private LoginController controller;

		private readonly AuthenticationRequestDTO authenticationRequest = new AuthenticationRequestDTO {Username = "username", Password = "password"};

		private const string PLUGINPATH = "/plugins";

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			authenticationService = mockery.DynamicMock<IAuthenticationService>();
			pluginsService = mockery.DynamicMock<IPluginsService>();
			formsAuthentication = mockery.Stub<IFormsAuthentication>();
			cultureService = mockery.DynamicMock<ICultureService>();
			controller = new LoginController(authenticationService, pluginsService, cultureService, formsAuthentication);

			ConfigurationManager.AppSettings["plugins.path"] = PLUGINPATH;

			PrepareController(controller);
		}

		[Test]
		public void IndexShouldRegisterPlugins()
		{
			With.Mocks(mockery)
				.Expecting(() => pluginsService.RegisterInstalledPlugins(PLUGINPATH))
				.Verify(() => controller.Index());
		}

		[Test]
		public void ShouldAlertUserForInvalidLogin()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(authenticationService.AuthenticateUser(authenticationRequest)).Return(new AuthenticatedUserDTO()))
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
			DoSuccessFullLogin(new AuthenticatedUserDTO { Id = Guid.NewGuid(), Type = UserType.User }, UserType.User, "/Home/Index.castle");
		}

		private void DoSuccessFullLogin(AuthenticatedUserDTO authenticatedUser, UserType role, string redirectPath)
		{
			var defaultCulture = new CultureDTO {Id = Guid.NewGuid(), Name = "English"};
			With.Mocks(mockery)
				.Expecting(() =>
					{
						Expect.Call(authenticationService.AuthenticateUser(authenticationRequest)).Return(authenticatedUser);
						Expect.Call(formsAuthentication.Encrypt(null)).IgnoreArguments();
						Expect.Call(cultureService.GetDefaultCulture()).Return(defaultCulture);
					})
				.Verify(() => controller.Login(authenticationRequest));

			Assert.IsTrue(Context.CurrentUser.Identity.IsAuthenticated);
			Assert.IsTrue(Context.CurrentUser.IsInRole(role.ToString()));
			Assert.AreEqual(defaultCulture, Context.Session["Culture"]);
			Assert.AreEqual(redirectPath, Response.RedirectedTo);
		}

	}
}