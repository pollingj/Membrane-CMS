using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;

namespace Membrane.Controllers
{
    [Layout("default"), Rescue("generalerror")]
    public class HomeController : ARSmartDispatcherController
    {
        public void Index()
        {
            
        }
    }
}