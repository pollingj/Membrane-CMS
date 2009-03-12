using Castle.MonoRail.Framework;
using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.Core.DTOs;

namespace Membrane.Controllers.MembraneAdmin
{
	/// <summary>
	/// Administration of the Navigation Types
	/// This will in all likelyhood inherit from a BaseCRUDController.
	/// </summary>
	[Layout("default"), Rescue("generalerror")]
	public class NavigationTypeController : BaseCrudController<NavigationTypeDTO>
	{
		public NavigationTypeController(IBaseCrudService<NavigationTypeDTO> service)
			: base(service)
		{
		}
	}
}