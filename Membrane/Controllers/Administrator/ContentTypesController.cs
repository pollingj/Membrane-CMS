using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
using Membrane.Commons;

namespace Membrane.Controllers.Administrator
{
	public class ContentTypesController : BaseController
	{
		private readonly IWindsorContainer container; 

		public ContentTypesController()
		{
			container = WindsorContainerAccessorUtil.ObtainContainer();
		}

		public void List()
		{
			var plugins = container.ResolveAll<IMembranePlugin>();

			foreach (var plugin in plugins)
			{
				
			}
		}
	}
}