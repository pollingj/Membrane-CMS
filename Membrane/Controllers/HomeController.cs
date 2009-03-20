using Membrane.Commons;

namespace Membrane.Controllers
{
    public class HomeController : BaseController
    {
        public void Index()
        {
            RenderView("Index");
        }
    }
}