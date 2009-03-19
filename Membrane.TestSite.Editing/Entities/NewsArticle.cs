using System;
using Membrane.Commons.Persistence;

namespace Membrane.TestSite.Editing.Entities
{
	public class NewsArticle : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual string Headline { get; set; }
		public virtual string Story { get; set; }
	}
}