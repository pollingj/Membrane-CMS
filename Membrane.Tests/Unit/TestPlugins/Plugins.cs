using System;
using Castle.Windsor;
using Membrane.Commons;

namespace Membrane.Tests.Unit.TestPlugins
{
	public class TestBlogPlugin : IMembranePlugin
	{
		public string Name
		{
			get { return "Test Blog"; }
		}

		public string Version
		{
			get { return "1.0.0"; }
		}

		public void Initialize()
		{
		}

		public void RemoveComponents(IWindsorContainer container)
		{
		}

		public void Install()
		{
		}

		public void Uninstall()
		{
		}

		public void Upgrade()
		{
		}

		public void RegisterComponents(IWindsorContainer container)
		{
		}
	}

	public class TestNewsPlugin : IMembranePlugin
	{
		public string Name
		{
			get { return "Test News"; }
		}

		public string Version
		{
			get { return "1.0.0"; }
		}

		public void Initialize()
		{
		}

		public void RemoveComponents(IWindsorContainer container)
		{
		}

		public void Install()
		{
		}

		public void Uninstall()
		{
		}

		public void Upgrade()
		{
		}

		public void RegisterComponents(IWindsorContainer container)
		{
		}
	}
}