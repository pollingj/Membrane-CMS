using Castle.MonoRail.Framework;
using Membrane.Commons.CRUD.Controllers;
using Membrane.Commons.CRUD.Services;
using Membrane.Commons.FormGeneration.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Filters;

namespace Membrane.Controllers.User
{
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
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