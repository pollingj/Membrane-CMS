using System;
using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public class NavigationNode : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual NavigationType Type { get; set; }
		public virtual NavigationNode Parent { get; set; }
		public virtual string Name { get; set; }
		public virtual string Title { get; set; }
		public virtual char AccessKey { get; set; }
		public virtual string ExternalUrl { get; set; }
        
	}
}