using System;
using Membrane.Commons.FormGeneration.Enums;

namespace Membrane.Commons.FormGeneration.Attributes
{
	public class FormFieldTypeAttribute : Attribute
	{
		public FieldType Type { get; set; }

		public FormFieldTypeAttribute()
		{
		}

		public FormFieldTypeAttribute(FieldType type)
		{
			Type = type;
		}
	}
}