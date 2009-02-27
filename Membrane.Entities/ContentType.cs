
using System.Collections.Generic;

namespace Membrane.Entities
{
	/// <summary>
	/// All content elements will have a content type.  
	/// A content type could be:
	///		- News Article
	///		- Product Item
	///		- Exhibition etc
	/// </summary>
	public class ContentType : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual string Controller { get; set; }
		public virtual string Model { get; set; }
		public virtual IList<ContentElement> Elements { get; set; }
	}
}