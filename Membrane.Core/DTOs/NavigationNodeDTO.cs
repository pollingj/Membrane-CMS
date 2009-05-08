
using Castle.Components.Validator;
using Membrane.Commons.Plug;

namespace Membrane.Core.DTOs
{
	public class NavigationNodeDTO : BaseVersionedAndOrderedDTO
	{
		[ValidateNonEmpty]
		public NavigationTypeDTO Type { get; set; }
		public NavigationNodeDTO Parent { get; set; }
		[ValidateNonEmpty]
		public string Title { get; set; }
		[ValidateLength(0, 1)]
		public char AccessKey { get; set; }
		public string ExternalUrl { get; set; }
	}
}