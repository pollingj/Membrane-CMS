using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public class Template : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual string ViewFile { get; set; }

	}
}