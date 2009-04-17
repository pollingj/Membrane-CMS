using System;
using Castle.Windsor;
using Membrane.Commons;

namespace Membrane.Plugins.News
{
	public class Plugin : IMembranePlugin
	{
		public string Name
		{
			get { return "News"; }
		}

		public void Initialize()
		{
		}

		public void RegisterComponents(IWindsorContainer container)
		{
		}

		public void Install()
		{
			// Run sql install scripts

            // Register any relevants componets with the container.
		}

		public void Uninstall()
		{
			// Run sql uninstall scripts

			// Remove any relevant components from the container.
		}

		public void Upgrade()
		{
			throw new NotImplementedException();
		}
	}
}