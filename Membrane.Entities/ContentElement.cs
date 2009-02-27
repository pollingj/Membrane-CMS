namespace Membrane.Entities
{
	public class ContentElement : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual string FriendlyURL { get; set; }
		public virtual ContentType Type { get; set; }
	}
}