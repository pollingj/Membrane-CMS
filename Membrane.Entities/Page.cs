using System.Collections.Generic;
using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public class Page : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual Template Template { get; set; }
		public virtual IList<Widget> Widgets { get; set; }
	}
}