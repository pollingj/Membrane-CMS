using Castle.Windsor;
using Membrane.Commons;

namespace Membrane.TestSite.Editing
{
	public class Plugin : IWindsorPlugin
	{
		public string Name
		{
			get { return "Product Catalog Plugin"; }
		}

		public void RegisterComponents(IWindsorContainer container)
		{
			//container.AddComponent<IProductService, ProductService>();
		}
	}

}