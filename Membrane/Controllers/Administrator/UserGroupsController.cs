using Castle.MonoRail.Framework;
using Membrane.Commons.CRUD.Controllers;
using Membrane.Commons.CRUD.Services;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Filters;

namespace Membrane.Controllers.Administrator
{
	/// <summary>
	/// The User Groups controller.  Inherits all of its functionality from the <see cref="CRUDController"/>.
	/// </summary>
	[ControllerDetails(Area = "Administrator")]
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class UserGroupsController : CRUDController<UserGroupDTO, UserGroup>
	{
		/// <summary>
		/// Constructor that sets up all of the <see cref="CRUDService"/> and <see cref="AutoGenerator"/> references
		/// </summary>
		/// <param name="service">The base CRUDService with the relevant DTO and Entities referenced</param>
		/// <param name="autoGenerator"></param>
		public UserGroupsController(ICRUDService<UserGroupDTO, UserGroup> service, IAutoGenerator<UserGroupDTO> autoGenerator)
			: base(service, autoGenerator)
		{
		}
	}
}



