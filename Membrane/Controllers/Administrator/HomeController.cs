using Castle.MonoRail.Framework;
using Membrane.Filters;

namespace Membrane.Controllers.Administrator
{
	/// <summary>
	/// Home controller for a Membrane Administrator (Super user)
	/// </summary>
	[ControllerDetails(Area="Administrator")]
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class HomeController : BaseController
	{
		public void Index()
		{
			
		}
	}
}