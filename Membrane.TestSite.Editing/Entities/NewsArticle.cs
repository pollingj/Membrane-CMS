using Membrane.Commons.Persistence;
using Membrane.Commons.Scaffolding;

namespace Membrane.TestSite.Editing.Entities
{
	public class NewsArticle : BaseModel
	{
		public virtual string Headline { get; set; }
		public virtual string Story { get; set; }
		[FieldTypeAttr(FieldType.SingleDropDownList, typeof(NewsType), "Id", "Name")]
		public virtual NewsType NewsType { get; set; }
	}
}