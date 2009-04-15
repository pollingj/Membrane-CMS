using System;
using Membrane.Commons.CRUD;

namespace Membrane.Plugins.News.DTOs
{
	public class NewsArticleDTO : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Story { get; set; }
		public DateTime CreationDate { get; set; }
	}
}