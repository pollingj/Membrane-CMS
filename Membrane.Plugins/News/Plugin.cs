using System;
using Castle.Core;
using Castle.Windsor;
using Membrane.Commons;
using Membrane.Plugins.News.Controllers;

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
			container.AddComponentLifeStyle("newscontroller", typeof(NewsController), LifestyleType.Transient);
		}

		public void RemoveComponents(IWindsorContainer container)
		{
			container.Kernel.RemoveComponent("newscontroller");
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