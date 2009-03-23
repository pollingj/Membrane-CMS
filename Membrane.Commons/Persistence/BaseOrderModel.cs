
namespace Membrane.Commons.Persistence
{
	/// <summary>
	/// This class is used when ordering is required on the objects
	/// This may need to be revisited to allow users to specify the ordering field
	/// </summary>
	public abstract class BaseOrderModel : BaseModel
	{
		public virtual int OrderPosition { get; set; }
	}
}