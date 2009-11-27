using Castle.MonoRail.Framework;

namespace Membrane.Plugins.News.Tags
{
	[ViewComponentDetails("news", Sections = "startrow,endrow")]
	public class News : ViewComponent
	{
		public override void Initialize()
		{
			throw new ViewComponentException("The AutomaticFormItemGenerator requires a view component parameter named 'fields' which should contain 'IList<FormField>' instance");

		}
	}
}