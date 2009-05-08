using System;
using Castle.Components.Validator;
using Membrane.Commons.Plugin.DTOs.Interfaces;

namespace Membrane.Commons.Plug
{
	public class BaseDTO : IDto
	{
		public Guid Id { get; set; }
		[ValidateNonEmpty]
		public string Name { get; set; }
	}
}