using System.Web;

namespace Membrane.Commons.Web
{
	public class HttpContextFacade : IHttpContext
	{
		public object GetItem(string key)
		{
			return HttpContext.Current.Items[key];
		}
	}

}