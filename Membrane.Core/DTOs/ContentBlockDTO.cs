using Castle.Components.Validator;
using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Core.DTOs
{
	public class ContentBlockDTO : BaseDTO
	{
		[ValidateNonEmpty]
		public string Name { get; set; }
		[FormFieldType(FieldType.TextEditor)]
		public string Content { get; set; }
	}
}