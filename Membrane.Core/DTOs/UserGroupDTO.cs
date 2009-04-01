using System;
using Castle.Components.Validator;

namespace Membrane.Core.DTOs
{
	public class UserGroupDTO
	{
		public Guid Id { get; set; }

		[ValidateNonEmpty]
		public string Name { get; set; }
	}
}