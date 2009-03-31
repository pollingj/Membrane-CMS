using System;
using System.Web.Security;
using Membrane.Core.Services.Interfaces;

namespace Membrane.Core.Services
{
	public class FormsAuthenticationWrapper : IFormsAuthentication
	{
		public void SignOut()
		{
			FormsAuthentication.SignOut();

		}

		public void SetAuthCookie(string email, bool createPersistentCookie)
		{
			throw new NotImplementedException();
		}

		public string Encrypt(FormsAuthenticationTicket ticket)
		{
			return FormsAuthentication.Encrypt(ticket);
		}

		public string FormsCookieName
		{
			get { return FormsAuthentication.FormsCookieName; }
		}
	}
}