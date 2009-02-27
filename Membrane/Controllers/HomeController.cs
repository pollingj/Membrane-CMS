using Membrane.Core.Services.Interfaces;

namespace Membrane.Controllers
{
    public class HomeController : BaseController
    {
		private readonly IContentService service;

		public HomeController(IContentService service)
        {
            this.service = service;
        }

        public void Index()
        {
			PropertyBag["List"] = service.GetCurrentElements("NavigationNode");
        	PropertyBag["Data"] = service.GetElementItem("NavigationNode", 2);
        }
    }
}