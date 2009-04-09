using System;
using System.Collections.Generic;
using Castle.MonoRail.Framework;
using Membrane.Commons.CRUD;
using Membrane.Commons.CRUD.Controllers;
using Membrane.Commons.CRUD.Services;
using Membrane.Commons.FormGeneration.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Filters;

namespace Membrane.Controllers.Administrator
{
	/// <summary>
	/// The Navigation Types controller.  Inherits all of its functionality from the <see cref="CRUDController"/>.
	/// </summary>
	[ControllerDetails(Area = "Administrator")]
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class NavigationTypesController : CRUDController<NavigationTypeDTO, NavigationType>
	{
		public NavigationTypesController(ICRUDService<NavigationTypeDTO, NavigationType> service, IAutoGenerator<NavigationTypeDTO> autoGenerator) : base(service, autoGenerator)
		{
			NavigationTypeDTO type = new NavigationTypeDTO {Id = Guid.NewGuid(), Name = "test"};

			var newtype = (IDTO)type;

			IList<NavigationTypeDTO> list = new IList<NavigationTypeDTO>();

			IList<IDTO> newList = list;
		}
	}
}