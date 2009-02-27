using System.Collections.Generic;

namespace Membrane.Entities
{
	public class NavigationType : BaseModel
	{
		public virtual string Name { get; set; }
		//public virtual IList<NavigationNode> Nodes { get; set; } 
	}
}