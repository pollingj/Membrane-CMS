using System;
using Castle.Components.Validator;
using Membrane.Commons.Plugin;

namespace Membrane.Core.DTOs
{
	public class NavigationTypeDTO : IDto
	{
		public Guid Id { get; set; }
		[ValidateNonEmpty]
		public string Name { get; set; }
	}
}