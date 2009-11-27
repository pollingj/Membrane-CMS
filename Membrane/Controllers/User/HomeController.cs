using Castle.MonoRail.Framework;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Filters;

namespace Membrane.Controllers.User
{
	/// <summary>
	/// Home controller for a standard membrane user
	/// </summary>
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class HomeController : BaseController
	{
		private readonly IPluginsService service;

		public HomeController(IPluginsService service)
		{
			this.service = service;
		}

		public void Index()
		{
			PropertyBag["InstalledPlugins"] = service.GetAllInstalledPlugins();
		}

		public void GoToPluginCreation(string controller)
		{
			Redirect(controller, "New");
		}
	}
}