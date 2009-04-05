using System;
using Castle.Components.Validator;
using Castle.MonoRail.Framework;
using Membrane.Core.DTOs;
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

		public void New()
		{
			PropertyBag["grouptype"] = typeof (UserGroupDTO);
		}

		public void Submit([DataBind("group", Validate = true)] UserGroupDTO group)
		{
			var submitError = false;

			if (Validator.IsValid(group))
			{
				// Are we handling a new or editted user group?
				if (group.Id == Guid.Empty)
				{
					var newId = service.Create(group);

					if (newId == Guid.Empty)
					{
						submitError = CreateError("There was a problem inserting this group.");
					}
				}
				else
				{
					var updateSuccess = service.Update(group);

					if (!updateSuccess)
					{
						submitError = CreateError("There was a problem updating this group.");
					}
				}
			}
			else
			{
				Flash["error"] = Validator.GetErrorSummary(group);
				submitError = true;
			}

			if (submitError)
			{
				Flash["group"] = group;
				RedirectToReferrer();
			}
			else
			{
				RedirectToAction("List");
			}

		}

		public void Edit(Guid id)
		{
			PropertyBag["group"] = service.GetUserGroup(id);
		}

		public void ConfirmDelete(Guid id)
		{
			PropertyBag["group"] = service.GetUserGroup(id);
		}

		public void Delete(Guid id)
		{
			if (service.Delete(id))
			{
				RedirectToAction("List");
			}
			else
			{
				CreateError("There was a problem deleting this group");
				Response.RedirectToReferrer();
			}
		}

		private bool CreateError(string errorMessage)
		{
			var errorSummary = new ErrorSummary();
			errorSummary.RegisterErrorMessage(string.Empty, errorMessage);
			Flash["error"] = errorSummary;
			return true;
		}
	}
}