using Castle.ActiveRecord;

namespace Membrane.Core.Domain
{
	/// <summary>
	/// This class is used when ordering is required on the objects
	/// This may need to be revisited to allow users to specify the ordering field
	/// </summary>
	public class BaseOrderModel : BaseModel
	{
		[Property]
		public int Order { get; set; }
	}
}