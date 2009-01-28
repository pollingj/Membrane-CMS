using Castle.MonoRail.Framework;
using Membrane.Models.NavigationManagement;
using Rhino.Commons;

namespace Membrane.Controllers
{
    [Layout("default"), Rescue("generalerror")]
    public class NavigationTypeController : SmartDispatcherController
    {
        public void ShowNavigationTypes()
        {
            PropertyBag["NavigationTypes"] = Repository<NavigationType>.FindAll();
        }
    }
}