using System;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Plugins.News.DTOs
{
	public class NewsArticleDTO : BaseVersionedDTO
	{
		public string Story { get; set; }
		public DateTime CreationDate { get; set; }
	}
}