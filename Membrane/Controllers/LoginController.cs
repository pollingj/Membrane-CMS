using System;
using System.Web;
using System.Web.Security;
using Castle.MonoRail.Framework;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities.Enums;

namespace Membrane.Controllers
{
	public class LoginController : BaseController
	{
		private readonly IAuthenticationService service;

		public LoginController(IAuthenticationService service)
		{
			this.service = service;
		}

		[AccessibleThrough(Verb.Post)]
		public void Login(string username, string password)
		{
			var authenticatedUser = service.AuthenticateUser(username, password);

			if (authenticatedUser != null)
			{
				CreateAuthenticationTicket(authenticatedUser);

				// Redirect to relevant page based on usertype
				switch (authenticatedUser.Type)
				{
					case UserType.Administrator:
						Redirect("Administrator", "Home", "Index");
						break;
					case UserType.User:
						Redirect("User", "Home", "Index");
						break;
				}
			}
			else
			{
				Flash["error"] = "Username or Password not recognised";
				RedirectToReferrer();	
			}
		}

		private void CreateAuthenticationTicket(AuthenticatedUserDTO user)
		{
			// Set up user
			var ticket = new FormsAuthenticationTicket(1, user.Id.ToString(), DateTime.Now,
																			 DateTime.Now.AddMinutes(30), true,
																			 user.Type.ToString(),
																			 FormsAuthentication.FormsCookiePath);

			var hash = FormsAuthentication.Encrypt(ticket);
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

			if (ticket.IsPersistent)
				cookie.Expires = ticket.Expiration;

			Context.CurrentUser = user;
		}
	}
}