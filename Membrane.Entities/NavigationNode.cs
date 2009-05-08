using System;
using Membrane.Commons.Plugin.Entities;

namespace Membrane.Entities
{
	public class NavigationNode : BaseVersionedAndOrderedEntity
	{
		public virtual NavigationType Type { get; set; }
		public virtual NavigationNode Parent { get; set; }
		public virtual string Name { get; set; }
		public virtual string Title { get; set; }
		public virtual char AccessKey { get; set; }
		public virtual string ExternalUrl { get; set; }
	}
}