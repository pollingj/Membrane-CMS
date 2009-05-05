
using Castle.Components.Validator;
using Castle.MonoRail.Framework;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Filters;

namespace Membrane.Controllers.User
{
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class UserDetailsController : BaseController
	{
		private readonly IUserService userService;
		private readonly IPropertyReaderService<UserDetailsRequestDTO> readerService;

		public UserDetailsController(IUserService userService, IPropertyReaderService<UserDetailsRequestDTO> readerService)
		{
			this.userService = userService;
			this.readerService = readerService;
		}

		public void Show()
		{
			var dataPropertyBagName = "details";
			PropertyBag["prefix"] = dataPropertyBagName;
			PropertyBag[dataPropertyBagName] = userService.LoadDetails(((AuthenticatedUserDTO)Session["user"]).Id);
			readerService.ReadViewModelProperties();
			PropertyBag["fields"] = readerService.FormFields;

			RenderView("/Shared/Form");
		}

		public void Update([DataBind("details", Validate = true)] UserDetailsRequestDTO details)
		{
			if (Validator.IsValid(details))
			{
				var success = userService.UpdateDetails(details);

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