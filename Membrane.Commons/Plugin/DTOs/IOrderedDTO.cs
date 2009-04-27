namespace Membrane.Commons.Plugin.DTOs
{
	public interface IOrderedDto : IDto
	{
		int OrderPosition { get; set; }
	}
}