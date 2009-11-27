using System;
using Castle.MonoRail.Framework;
using Castle.MonoRail.TestSupport;
using Membrane.Controllers.Administrator;
using Membrane.Core.DTOs;
using Membrane.Entities.Enums;
using Membrane.Filters;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Web.MonoRail.Filters
{
	[TestFixture]
	public class AuthenticationFilterFixture : BaseControllerTest
	{
		private HomeController controller;
		private AuthenticationFilter filter;

		[SetUp]
		public void SetUp()
		{
			controller = new HomeController();

			filter = new AuthenticationFilter();

			PrepareController(controller);
		}

		[Test]
		public void LetAdministratorAccessHomeController()
		{
			Context.CurrentUser = new AuthenticatedUserDTO { Id = Guid.NewGuid(), Type = UserType.Administrator };
			Context.Session["user"] = Context.CurrentUser;
			Assert.IsTrue(filter.Perform(ExecuteWhen.BeforeAction, Context, Context.CurrentController, Context.CurrentControllerContext));
		}

		[Test]
		public void NonLoggedInUserCannotAccessHomeController()
		{
			Assert.IsFalse(filter.Perform(ExecuteWhen.BeforeAction, Context, Context.CurrentController, Context.CurrentControllerContext));
			Assert.AreEqual("/", Response.RedirectedTo);

		}
	}
}