using System;
using Membrane.Commons.FormGeneration.Enums;

namespace Membrane.Commons.FormGeneration.Attributes
{
	public class FormFieldTypeAttribute : Attribute
	{
		public FieldType Type { get; set; }
		public string OptionValue { get; set; }
		public string OptionText { get; set; }

		public FormFieldTypeAttribute()
		{
		}

		public FormFieldTypeAttribute(FieldType type)
		{
			Type = type;
		}

		public FormFieldTypeAttribute(FieldType type, string optionValue, string optionText)
		{
			Type = type;
			OptionValue = optionValue;
			OptionText = optionText;
		}
	}
}