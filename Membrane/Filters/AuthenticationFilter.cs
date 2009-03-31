using Castle.MonoRail.Framework;
using Membrane.Core.DTOs;
using Membrane.Entities.Enums;

namespace Membrane.Filters
{
	public class AuthenticationFilter : IFilter
	{
		private readonly UserType userType;

		public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller, IControllerContext controllerContext)
		{
			// Read previous authenticated principal from session 
			// (could be from cookie although with more work)

			var user = (AuthenticatedUserDTO)context.Session["user"];

			// Sets the principal as the current user
			context.CurrentUser = user;

			// Checks if it is OK
			if (context.CurrentUser == null || !context.CurrentUser.Identity.IsAuthenticated)
			{
				// Not authenticated, redirect to login
				context.Response.RedirectToSiteRoot();

				// Prevent request from continue
				return false;
			}

			// Everything is ok
			return true;
		}
	}
}