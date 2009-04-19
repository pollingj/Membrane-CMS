using Castle.MonoRail.Framework;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Interfaces;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.Services;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Filters;

namespace Membrane.Controllers.Administrator
{
	/// <summary>
	/// The User Groups controller.  Inherits all of its functionality from the <see cref="CRUDController{TDto,TEntity}"/>.
	/// </summary>
	[ControllerDetails(Area = "Administrator")]
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class UserGroupsController : CRUDController<UserGroupDTO, UserGroup>
	{
		/// <summary>
		/// Constructor that sets up all of the <see cref="CRUDService{TDto,TEntity}"/> and <see cref="PropertyReaderService"/> references
		/// </summary>
		/// <param name="service">The base CRUDService with the relevant TDto and Entities referenced</param>
		/// <param name="propertyReaderService"></param>
		public UserGroupsController(ICRUDService<UserGroupDTO, UserGroup> service, IPropertyReaderService<UserGroupDTO> propertyReaderService)
			: base(service, propertyReaderService)
		{
		}
	}
}



