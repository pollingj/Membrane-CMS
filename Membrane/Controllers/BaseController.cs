using Castle.MonoRail.Framework;

namespace Membrane.Controllers
{
	/// <summary>
	/// Base Controller that everything runs off.
	/// </summary>
	[Layout("default"), Rescue("generalerror")]
	public class BaseController : SmartDispatcherController
	{

	}
}