using System;
using Membrane.Commons.Persistence;

namespace Membrane.Plugins.News.Entities
{
	public class NewsArticle : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Story { get; set; }
		public virtual string CreationDate { get; set; }
	}
}