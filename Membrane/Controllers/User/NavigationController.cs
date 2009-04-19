using Castle.MonoRail.Framework;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.Services;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Filters;

namespace Membrane.Controllers.User
{
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class NavigationController : CRUDController<NavigationNodeDTO, NavigationNode>
	{
		private readonly ICRUDService<NavigationTypeDTO, NavigationType> navTypeService;

		public NavigationController(ICRUDService<NavigationNodeDTO, NavigationNode> service, IPropertyReaderService<NavigationNodeDTO> propertyReaderService, ICRUDService<NavigationTypeDTO, NavigationType> navTypeService)
			: base(service, propertyReaderService)
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