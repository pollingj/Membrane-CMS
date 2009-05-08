namespace Membrane.Commons.Plugin.DTOs.Interfaces
{
	public interface IOrderedDto : IDto
	{
		int OrderPosition { get; set; }
	}
}