using System.Collections.Generic;
using Castle.MonoRail.Framework;
using Membrane.Commons.CRUD.Controllers;
using Membrane.Commons.CRUD.Services;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Filters;

namespace Membrane.Controllers.Administrator
{
	[ControllerDetails(Area = "Administrator")]
	[Filter(ExecuteWhen.BeforeAction, typeof(AuthenticationFilter))]
	public class UserGroupsController : CRUDController<UserGroupDTO, UserGroup>
	{
		public UserGroupsController(ICRUDService<UserGroupDTO, UserGroup> service, IAutoGenerator<UserGroupDTO> autoGenerator)
			: base(service, autoGenerator)
		{
		}

		public override void New()
		{
			PropertyBag["fields"] = new List<FormField>
			                        	{
			                        		new FormField {Id = "Test", Label = "Test", Type = FieldType.SingleLineTextField}
			                        	};

			base.New();


		}
	}
}



