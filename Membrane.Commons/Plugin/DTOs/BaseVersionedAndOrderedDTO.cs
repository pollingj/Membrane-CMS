using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.Plugin.DTOs.Interfaces;

namespace Membrane.Commons.Plugin.DTOs
{
	public class BaseVersionedAndOrderedDTO : BaseVersionedDTO, IOrderedDto
	{
		[FormFieldType(FieldType.Hidden)]
		public int OrderPosition { get; set; }
	}
}