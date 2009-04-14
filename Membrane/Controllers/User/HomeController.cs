using Castle.MonoRail.Framework;
using Membrane.Filters;

namespace Membrane.Controllers.User
{
	/// <summary>
	/// Home controller for a standard membrane user
	/// </summary>
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class HomeController : BaseController
	{
		public void Index()
		{

		}
	}
}