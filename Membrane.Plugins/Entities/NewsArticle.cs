using System;
using Membrane.Commons.Persistence;

namespace Membrane.Plugins.Entities
{
	public class NewsArticle : IEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Story { get; set; }
		public string CreationDate { get; set; }
	}
}