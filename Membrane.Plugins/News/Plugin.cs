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

		public string Version
		{
			get { return "1.0.0"; }
		}


		public void Initialize()
		{
		}

		public void RegisterComponents(IWindsorContainer container)
		{
		}

		public void RemoveComponents(IWindsorContainer container)
		{
		}

		public void Install()
		{
			// Run sql install scripts


		}

		public void Uninstall()
		{
			// Run sql uninstall scripts

		}

		public void Upgrade()
		{
			throw new NotImplementedException();
		}
	}
}