using Castle.Components.Validator;
using Membrane.Commons.Plugin;

namespace Membrane.Controllers.Administrator
{
	public class PluginsController : BaseController
	{
		private readonly IPluginsService service;

		public PluginsController(IPluginsService service)
		{
			this.service = service;
		}

		public void List()
		{
			PropertyBag["plugins"] = service.FindAvailablePlugins();
		}

		public void Install(string pluginName)
		{
			var installed = service.InstallPlugin(pluginName);

			PluginActionHandling(installed, "installing");
		}

		public void Uninstall(string pluginName)
		{
			var uninstalled = service.UninstallPlugin(pluginName);

			PluginActionHandling(uninstalled, "uninstalling");
		}

		public void Upgrade(string pluginName)
		{
			var upgraded = service.UpgradePlugin(pluginName);

			PluginActionHandling(upgraded, "upgrading");
		}

		private void PluginActionHandling(bool success, string action)
		{
			if (!success)
			{
				CreateErrorMessage(string.Format("There was a problem {0} this plugin", action));
			}
			RedirectToReferrer();
		}

		private void CreateErrorMessage(string message)
		{
			var errorSummary = new ErrorSummary();
			errorSummary.RegisterErrorMessage(string.Empty, message);
			Flash["error"] = errorSummary;
		}


	}
}