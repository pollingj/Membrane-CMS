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
	}
}