using Membrane.Commons.CRUD.Controllers;
using Membrane.Commons.CRUD.Services;
using Membrane.Commons.FormGeneration.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;

namespace Membrane.Controllers.User
{
	public class NavigationController : CRUDController<NavigationNodeDTO, NavigationNode>
	{
		private readonly ICRUDService<NavigationTypeDTO, NavigationType> navTypeService;

		public NavigationController(ICRUDService<NavigationNodeDTO, NavigationNode> service, IAutoGenerator<NavigationNodeDTO> autoGenerator, ICRUDService<NavigationTypeDTO, NavigationType> navTypeService)
			: base(service, autoGenerator)
		{
			this.navTypeService = navTypeService;
		}

		public override void LoadSupportiveData()
		{
			PropertyBag["support.NavigationTypeDTO"] = navTypeService.GetItems();
			PropertyBag["support.NavigationNodeDTO"] = Service.GetItems();
		}
	}
}