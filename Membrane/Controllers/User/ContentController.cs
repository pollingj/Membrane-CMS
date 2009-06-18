using Castle.MonoRail.Framework;
using Membrane.Core.Services.Interfaces;

namespace Membrane.Controllers.User
{
	public class ContentController : BaseController
	{
		private readonly IPluginsService pluginsService;

		public ContentController(IPluginsService pluginsService)
		{
			this.pluginsService = pluginsService;
		}

		[DefaultAction]
		public void GoToFirstPluginList()
		{
			var plugins = pluginsService.GetAllInstalledPlugins();

			if (plugins.Count > 0)
				Redirect(plugins[0].Name, "List");
			else
				PropertyBag["NoPlugins"] = true;
		}
	}
}