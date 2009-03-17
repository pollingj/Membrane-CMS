using Castle.MonoRail.Framework;
using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.Entities;

namespace Membrane.Controllers.MembraneAdmin
{
	/// <summary>
	/// Administration of the Navigation Types
	/// This will in all likelyhood inherit from a BaseCRUDController.
	/// </summary>
	[Layout("default"), Rescue("generalerror")]
	public class NavigationTypeController : BaseCrudController<NavigationType>
	{
		public NavigationTypeController(IBaseCrudService<NavigationType> service)
			: base(service)
		{
		}
	}
}