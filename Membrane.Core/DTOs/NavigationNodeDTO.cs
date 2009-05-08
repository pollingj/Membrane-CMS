using System;
using Castle.Components.Validator;
using Membrane.Commons.Plugin.DTOs.Interfaces;

namespace Membrane.Core.DTOs
{
	public class NavigationNodeDTO : IOrderedDto
	{
		public Guid Id { get; set; }
		[ValidateNonEmpty]
		public string Name { get; set; }
		[ValidateNonEmpty]
		public NavigationTypeDTO Type { get; set; }
		public NavigationNodeDTO Parent { get; set; }
		[ValidateNonEmpty]
		public string Title { get; set; }
		[ValidateLength(0, 1)]
		public char AccessKey { get; set; }
		public string ExternalUrl { get; set; }
		public int OrderPosition { get; set; }
	}
}