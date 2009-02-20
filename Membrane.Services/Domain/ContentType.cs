using Castle.ActiveRecord;

namespace Membrane.Core.Domain
{
	/// <summary>
	/// All content elements will have a content type.  
	/// A content type could be:
	///		- News Article
	///		- Product Item
	///		- Exhibition etc
	/// </summary>
	[ActiveRecord]
	public class ContentType : BaseModel
	{
		[Property(NotNull = true, Length = 200)]
		public string Name { get; set; }

		[Property(NotNull = true, Length = 200)]
		public string Controller { get; set; }

		[Property(NotNull = true, Length = 200)]
		public string Model { get; set; }
	}
}