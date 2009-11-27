using Membrane.Commons.FormGeneration.Enums;

namespace Membrane.Commons.FormGeneration
{
	public class FormField
	{
		public string Id { get; set;  }
		public string Label { get; set; }
		public FieldType? Type { get; set; }
		public string OptionValue { get; set; }
		public string OptionText { get; set; }
		public int? FieldOrder { get; set; }
		public string RelatedTypeName { get; set; }
	}
}