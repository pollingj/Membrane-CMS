using System;
using Membrane.Commons.FormGeneration.Enums;

namespace Membrane.Commons.FormGeneration.Attributes
{
	public class FormFieldTypeAttribute : Attribute
	{
		public FieldType? Type { get; set; }
		public string OptionValue { get; set; }
		public string OptionText { get; set; }
		public int FieldOrder { get; set; }

		public FormFieldTypeAttribute()
		{
		}

		public FormFieldTypeAttribute(int fieldOrder)
		{
			FieldOrder = fieldOrder;
		}

		public FormFieldTypeAttribute(FieldType type)
		{
			Type = type;
		}

		public FormFieldTypeAttribute(FieldType type, int fieldOrder) : this(fieldOrder)
		{
			Type = type;
		}

		public FormFieldTypeAttribute(FieldType type, string optionValue, string optionText) : this(type)
		{
			OptionValue = optionValue;
			OptionText = optionText;
		}

		public FormFieldTypeAttribute(FieldType type, string optionValue, string optionText, int fieldOrder) : this(type, fieldOrder)
		{
			OptionValue = optionValue;
			OptionText = optionText;
		}
	}
}