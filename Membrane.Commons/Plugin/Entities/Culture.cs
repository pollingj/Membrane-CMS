namespace Membrane.Commons.Plugin.Entities
{
	public class Culture : BaseEntity
	{
		public virtual string Language { get; set; }
		public virtual string ShortCode { get; set; }
		public virtual bool IsDefault { get; set; }
	}
}