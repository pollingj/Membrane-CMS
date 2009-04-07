using System;
using Membrane.Commons.FormGeneration.Enums;

namespace Membrane.Commons.FormGeneration
{
	public class FormField
	{
		public string Id { get; set;  }
		public string Label { get; set; }
		public FieldType Type { get; set; }
	}
}