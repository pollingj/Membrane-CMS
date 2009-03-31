using Castle.MonoRail.Framework;
using Membrane.Core.Services.Interfaces;
using Membrane.Filters;

namespace Membrane.Controllers.Administrator
{
	[ControllerDetails(Area = "Administrator")]
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class UserGroupsController : BaseController
	{
		private readonly IUserGroupService service;
		private const int defaultPageNumber = 1;
		private const int defaultPageSize = 10;

		public UserGroupsController(IUserGroupService service)
		{
			this.service = service;	
		}

		public void List()
		{
			List(defaultPageNumber, defaultPageSize);
		}

		public void List(int currentPage, int pageSize)
		{
			PropertyBag["groups"] = service.GetPagedUserGroups(currentPage, pageSize);
		}
	}
}