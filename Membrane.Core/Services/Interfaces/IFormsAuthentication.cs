using System.Web.Security;

namespace Membrane.Core.Services.Interfaces
{
	public interface IFormsAuthentication
	{
		void SignOut();
		string Encrypt(FormsAuthenticationTicket ticket);
		string FormsCookieName { get; }
	}
}