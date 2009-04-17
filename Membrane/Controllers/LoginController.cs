using System;
using System.Web;
using System.Web.Security;
using Castle.MonoRail.Framework;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Core.Wrappers.Interfaces;
using Membrane.Entities.Enums;

namespace Membrane.Controllers
{
	/// <summary>
	/// Controller to handle all of the Login actions
	/// </summary>
	public class LoginController : BaseController
	{
		private readonly IAuthenticationService service;
		private readonly IFormsAuthentication formsAuthentication;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="service">The <see cref="AuthenticationService"/></param>
		/// <param name="formsAuthentication">The <see cref="FormsAuthentication"/> wrapper</param>
		public LoginController(IAuthenticationService service, IFormsAuthentication formsAuthentication)
		{
			this.service = service;
			this.formsAuthentication = formsAuthentication;
		}

		/// <summary>
		/// The Initial Index action - does nothing
		/// </summary>
		public void Index(){}

		/// <summary>
		/// Login action for when the user posts their login credentials
		/// </summary>
		/// <param name="authenticationRequest">The <see cref="AuthenticationRequestDTO"/></param>
		[AccessibleThrough(Verb.Post)]
		public void Login([DataBind("login")]AuthenticationRequestDTO authenticationRequest)
		{
			var authenticatedUser = service.AuthenticateUser(authenticationRequest);

			if (authenticatedUser.Id != Guid.Empty)
			{
				createAuthenticationTicket(authenticatedUser);

				// Redirect to relevant page based on usertype
				switch (authenticatedUser.Type)
				{
					case UserType.Administrator:
						Redirect("Administrator", "Home", "Index");
						break;
					case UserType.User:
						Redirect("Home", "Index");
						break;
				}
			}
			else
			{
				Flash["error"] = "Username or Password not recognised";
				RedirectToReferrer();	
			}
		}

		private void createAuthenticationTicket(AuthenticatedUserDTO user)
		{
			// Set up user
			var ticket = new FormsAuthenticationTicket(1, user.Id.ToString(), DateTime.Now,
																			 DateTime.Now.AddMinutes(30), true,
																			 user.Type.ToString(),
																			 FormsAuthentication.FormsCookiePath);

			var hash = formsAuthentication.Encrypt(ticket);
			var cookie = new HttpCookie(formsAuthentication.FormsCookieName, hash);

			if (ticket.IsPersistent)
				cookie.Expires = ticket.Expiration;

			Context.CurrentUser = user;
			Context.Session["user"] = user;
		}
	}
}