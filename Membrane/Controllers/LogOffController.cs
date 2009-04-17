using System.Web.Security;
using Membrane.Core.Wrappers.Interfaces;

namespace Membrane.Controllers
{
	/// <summary>
	/// Controller to handle a user logging off Membrane
	/// </summary>
	public class LogOffController : BaseController
	{
		private readonly IFormsAuthentication formsAuthentication;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="formsAuthentication">The <see cref="FormsAuthentication"/> Wrapper</param>
		public LogOffController(IFormsAuthentication formsAuthentication)
		{
			this.formsAuthentication = formsAuthentication;
		}

		/// <summary>
		/// Signs a user out and redirects to Membranes home page
		/// </summary>
		public void LogOff()
		{
			formsAuthentication.SignOut();
			Session.Clear();

			RedirectToSiteRoot();
		}
	}
}