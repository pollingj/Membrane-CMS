using Membrane.Commons.Plugin.DTOs.Interfaces;

namespace Membrane.Commons.Plug
{
	public class BaseOrderedDTO : BaseDTO, IOrderedDto
	{
		public int OrderPosition { get; set; }
	}
}