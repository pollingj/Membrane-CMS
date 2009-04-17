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
			throw new NotImplementedException();
		}

		public void Uninstall()
		{
			throw new NotImplementedException();
		}

		public void Upgrade()
		{
			throw new NotImplementedException();
		}
	}
}