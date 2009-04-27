namespace Membrane.Commons.Plugin
{
	public interface IOrderedDto : IDto
	{
		int OrderPosition { get; set; }
	}
}