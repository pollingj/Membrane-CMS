using System;
using Castle.Components.Validator;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Core.DTOs
{
	public class UserGroupDTO : IDto
	{
		public Guid Id { get; set; }

		[ValidateNonEmpty]
		public string Name { get; set; }
	}
}