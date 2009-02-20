using Castle.ActiveRecord;

namespace Membrane.Core.Domain
{
	public class NavigationType : BaseModel
	{
		[Property(Length = 300, NotNull = true)]
		public string Name { get; set; }
	}
}