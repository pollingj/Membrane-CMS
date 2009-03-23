using Membrane.Commons.Persistence;

namespace Membrane.TestSite.Editing.Entities
{
	public class NewsArticle : BaseModel
	{
		public virtual string Headline { get; set; }
		public virtual string Story { get; set; }
		public virtual NewsType NewsType { get; set; }
	}
}