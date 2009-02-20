using Castle.ActiveRecord;

namespace Membrane.Core.Domain
{
	[ActiveRecord]
	public class NavigationNode : BaseOrderModel
	{
		[BelongsTo("NavigationTypeID")]
		public NavigationType Type { get; set; }

		[Property (NotNull = true, Length = 200)]
		public string Name { get; set; }

		[Property(NotNull = true, Length = 200)]
		public string HTMLTitle { get; set; }

		[Property]
		public char AccessKey { get; set; }

		[Property(Length = 300)]
		public string Url { get; set; }
	}
}