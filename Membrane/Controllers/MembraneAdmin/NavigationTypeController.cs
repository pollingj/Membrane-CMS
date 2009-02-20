using Castle.MonoRail.Framework;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;

namespace Membrane.Controllers.MembraneAdmin
{
	/// <summary>
	/// Administration of the Navigation Types
	/// This will in all likelyhood inherit from a BaseCRUDController.
	/// </summary>
	[Layout("default"), Rescue("generalerror")]
	public class NavigationTypeController : BaseController
	{
		private readonly INavigationService service;

		public NavigationTypeController(INavigationService service)
		{
			this.service = service;
		}


		public void List()
		{
			PropertyBag["NavigationTypes"] = service.GetAllNavigationTypes();
		}

		public void Create()
		{
			PropertyBag["NavigationTypeType"] = typeof(NavigationTypeDTO);
		}



		public void Submit([DataBind("NavigationType", Validate = true)] NavigationTypeDTO navType)
		{
			var isOk = false;

			if (HasValidationError(navType))
			{
				Flash["ErrorSummary"] = GetErrorSummary(navType);
			}
			else
			{
				isOk = service.AddNavigationType(navType);

				if (!isOk)
					Flash["AddFailure"] = true;
			}

			if (isOk)
				RedirectToAction("List");
			else
			{
				Flash["NavigationType"] = navType;
				RedirectToReferrer();
			}
		}
	}
}