using System;
using System.Collections.Generic;
using Castle.Components.Validator;
using Castle.MonoRail.Framework;
using Membrane.Commons.CRUD.Controllers;
using Membrane.Commons.CRUD.Services;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;
using Membrane.Filters;

namespace Membrane.Controllers.Administrator
{
	[ControllerDetails(Area = "Administrator")]
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class UserGroupsController : CRUDController<UserGroupDTO, UserGroup>
	{
		public UserGroupsController(ICRUDService<UserGroupDTO, UserGroup> service) : base(service)
		{
		}
	}
}



