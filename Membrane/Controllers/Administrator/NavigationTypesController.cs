using Castle.MonoRail.Framework;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Filters;

namespace Membrane.Controllers.Administrator
{
	/// <summary>
	/// The Navigation Types controller.  Inherits all of its functionality from the <see cref="CRUDController{TDto,TEntity}"/>.
	/// </summary>
	[ControllerDetails(Area = "Administrator")]
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class NavigationTypesController : CRUDController<NavigationTypeDTO, NavigationType>
	{
		public NavigationTypesController(ICRUDService<NavigationTypeDTO, NavigationType> service, IPropertyReaderService<NavigationTypeDTO> propertyReaderService)
			: base(service, propertyReaderService)
		{
		}
	}
}