using System;
using Castle.Components.Validator;
using Membrane.Commons.CRUD;

namespace Membrane.Core.DTOs
{
	public class NavigationTypeDTO : IDTO
	{
		public Guid Id { get; set; }
		[ValidateNonEmpty]
		public string Name { get; set; }
	}
}