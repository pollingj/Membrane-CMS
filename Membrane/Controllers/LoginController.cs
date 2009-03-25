using System;
using Castle.MonoRail.Framework;
using Membrane.Core.Services.Interfaces;

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
				
			}
			else
			{
				Flash["error"] = "Username or Password not recognised";
				RedirectToReferrer();	
			}
		}
	}
}