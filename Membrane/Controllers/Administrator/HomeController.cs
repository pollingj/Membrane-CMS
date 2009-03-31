using Castle.MonoRail.Framework;
using Membrane.Filters;

namespace Membrane.Controllers.Administrator
{
	[ControllerDetails(Area="Administrator")]
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class HomeController : BaseController
	{
		public void Index()
		{
			
		}
	}
}