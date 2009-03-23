using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public class NavigationNode : BaseOrderModel
	{
		public virtual NavigationType Type { get; set; }
		public virtual ContentElement Element { get; set; }
		public virtual string Name { get; set; }
		public virtual string HTMLTitle { get; set; }
		public virtual char AccessKey { get; set; }
		public virtual string Url { get; set; }
	}
}