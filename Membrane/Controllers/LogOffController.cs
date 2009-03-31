using System.Web.Security;
using Membrane.Core.Services.Interfaces;

namespace Membrane.Controllers
{
	public class LogOffController : BaseController
	{
		private readonly IFormsAuthentication formsAuthentication;

		public LogOffController(IFormsAuthentication formsAuthentication)
		{
			this.formsAuthentication = formsAuthentication;
		}

		public void LogOff()
		{
			formsAuthentication.SignOut();
			Session.Clear();

			RedirectToSiteRoot();
		}
	}
}