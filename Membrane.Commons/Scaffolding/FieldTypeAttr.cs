using System;

namespace Membrane.Commons.Scaffolding
{
	[AttributeUsage(AttributeTargets.Property)]
	public class FieldTypeAttr : Attribute
	{
		// Properties
		public int ImageHeight { get; set; }
		public int ImageWidth { get; set; }
		public string Label { get; set; }
		public int MaxLength { get; set; }
		public Type Options { get; set; }
		public string OptionText { get; set; }
		public string OptionValue { get; set; }
		public FieldType Type { get; set; }
		public string UploadFolder { get; set; }
		
		// Methods
		public FieldTypeAttr(FieldType type)
		{
			Type = type;
		}

		public FieldTypeAttr(FieldType type, string label)
			: this(type)
		{
			Label = label;
		}

		public FieldTypeAttr(FieldType type, string label, int maxLength)
			: this(type, label)
		{
			MaxLength = maxLength;
		}

		public FieldTypeAttr(FieldType type, string label, string uploadFolder)
			: this(type, label)
		{
			UploadFolder = uploadFolder;
		}

		public FieldTypeAttr(FieldType type, Type options, string optionValue, string optionText)
			: this(type)
		{
			Options = options;
			OptionValue = optionValue;
			OptionText = optionText;
		}

		public FieldTypeAttr(FieldType type, string label, string uploadFolder, int imageWidth, int imageHeight)
			: this(type, label, uploadFolder)
		{
			ImageWidth = imageWidth;
			ImageHeight = imageHeight;
		}
	}
}