
using Castle.Components.Validator;
using Castle.MonoRail.Framework;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;

namespace Membrane.Controllers.User
{
	public class UserDetailsController : BaseController
	{
		private IUserService service;

		public UserDetailsController(IUserService service)
		{
			this.service = service;
		}

		public void Show()
		{
			PropertyBag["details"] = service.LoadDetails(((AuthenticatedUserDTO) Session["user"]).Id);
		}

		public void Update([DataBind("details", Validate = true)] UserDetailsRequestDTO details)
		{
			if (Validator.IsValid(details))
			{
				var success = service.UpdateDetails(details);

				if (!success)
				{
					var errorSummary = new ErrorSummary();
					errorSummary.RegisterErrorMessage(string.Empty, "There was a problem updating your details");
					Flash["errors"] = errorSummary;
				}
			}
			else
				Flash["errors"] = Validator.GetErrorSummary(details);

			RedirectToAction("Show");
		}
	}
}