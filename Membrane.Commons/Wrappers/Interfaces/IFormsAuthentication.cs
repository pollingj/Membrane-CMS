using System.Web.Security;

namespace Membrane.Commons.Wrappers.Interfaces
{
	public interface IFormsAuthentication
	{
		void SignOut();
		string Encrypt(FormsAuthenticationTicket ticket);
		string FormsCookieName { get; }
	}
}