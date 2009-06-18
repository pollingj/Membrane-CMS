using System;
using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Plugins.News.DTOs
{
	public class NewsArticleDTO : BaseVersionedDTO
	{
		[FormFieldType(FieldType.TextEditor)]
		public string Story { get; set; }
		public DateTime CreationDate { get; set; }
	}
}