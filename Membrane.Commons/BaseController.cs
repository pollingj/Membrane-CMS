using Castle.MonoRail.Framework;

namespace Membrane.Commons
{
	[Layout("default"), Rescue("generalerror")]
	public class BaseController : SmartDispatcherController
	{
	}
}