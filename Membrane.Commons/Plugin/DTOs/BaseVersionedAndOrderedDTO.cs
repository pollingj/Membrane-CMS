using Membrane.Commons.Plugin.DTOs.Interfaces;

namespace Membrane.Commons.Plug
{
	public class BaseVersionedAndOrderedDTO : BaseVersionedDTO, IOrderedDto
	{
		public int OrderPosition { get; set; }
	}
}