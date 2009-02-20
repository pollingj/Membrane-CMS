using Castle.Components.Validator;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.Framework.Services;
using Castle.MonoRail.Framework.Test;
using Castle.MonoRail.TestSupport;
using Rhino.Mocks;

namespace Membrane.Test.Controllers
{
	public class BaseControllerFixture : BaseControllerTest
	{
		public MockRepository mockery;
		private string referrer = "http://www.example.com";

		protected override IMockRequest BuildRequest()
		{
			StubRequest request = new StubRequest(Cookies);
			request.UrlReferrer = referrer;

			return request;
		}

		protected override IMockResponse BuildResponse(UrlInfo info)
		{
			StubResponse response = new StubResponse(info,
											new DefaultUrlBuilder(),
											new StubServerUtility(),
											new RouteMatch(),
											referrer);
			return response;
		}

		public virtual void SetUp()
		{
			mockery = new MockRepository();
		}
	}


}