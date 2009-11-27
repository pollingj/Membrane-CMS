using System;
using Castle.Components.Validator;
using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.Plugin.DTOs.Interfaces;

namespace Membrane.Commons.Plugin.DTOs
{
	public class BaseDTO : IDto
	{
		public Guid Id { get; set; }
		[ValidateNonEmpty]
		[FormFieldType(FieldOrder = 0)]
		public string Name { get; set; }
	}
}