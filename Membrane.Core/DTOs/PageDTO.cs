using System.Collections.Generic;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Core.DTOs
{
	public class PageDTO : BaseDTO
	{
		public string Name { get; set; }
		public TemplateDTO Template { get; set; }
		public IList<ContentBlockDTO> ContentBlocks { get; set; }
	}
}