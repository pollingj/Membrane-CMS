using Castle.MonoRail.Framework;
using Membrane.Commons.FormGeneration.Services;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.Services;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Filters;

namespace Membrane.Controllers.User
{
	/// <summary>
	/// The User Groups controller.  Inherits all of its functionality from the <see cref="CRUDController{TDto,TEntity}"/>.
	/// </summary>
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class UsersController : CRUDController<UserDetailsRequestDTO, MembraneUser>
	{
		/// <summary>
		/// Constructor that sets up all of the <see cref="CRUDService{TDto,TEntity}"/> and <see cref="PropertyReaderService{T}"/> references
		/// </summary>
		/// <param name="service">The base CRUDService with the relevant TDto and Entities referenced</param>
		/// <param name="propertyReaderService"></param>
		public UsersController(ICRUDService<UserDetailsRequestDTO, MembraneUser> service, IPropertyReaderService<UserDetailsRequestDTO> propertyReaderService)
			: base(service, propertyReaderService)
		{
		}
	}
}