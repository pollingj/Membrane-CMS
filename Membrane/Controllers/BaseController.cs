using Castle.MonoRail.Framework;

namespace Membrane.Controllers
{
	[Layout("default"), Rescue("generalerror")]
	public class BaseController : SmartDispatcherController
	{
	}
}