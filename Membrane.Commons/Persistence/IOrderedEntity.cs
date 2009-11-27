namespace Membrane.Commons.Persistence
{
	public interface IOrderedEntity : IEntity
	{
		int OrderPosition { get; set; }
	}
}