using System;
using System.Configuration;
using Castle.Components.Validator;
using Membrane.Core.Services.Interfaces;

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
			PropertyBag["plugins"] = service.FindAvailablePlugins(ConfigurationManager.AppSettings["plugins.path"]);
			PropertyBag["installedplugins"] = service.GetAllInstalledPlugins();
		}

		public void Install(string pluginName)
		{
			var installed = service.InstallPlugin(pluginName, ConfigurationManager.AppSettings["plugins.path"]);

			PluginActionHandling(installed, "installing");
		}

		public void Uninstall(Guid pluginId)
		{
			var uninstalled = service.UninstallPlugin(pluginId, ConfigurationManager.AppSettings["plugins.path"]);

			PluginActionHandling(uninstalled, "uninstalling");
		}

		public void Upgrade(Guid pluginId)
		{
			var upgraded = service.UpgradePlugin(pluginId);

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