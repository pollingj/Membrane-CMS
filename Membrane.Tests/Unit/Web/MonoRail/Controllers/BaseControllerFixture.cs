using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.Framework.Services;
using Castle.MonoRail.Framework.Test;
using Castle.MonoRail.TestSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers
{
	public class BaseControllerFixture : BaseControllerTest
	{
		public string Referrer = "http://www.project.com/index.castle";

		public MockRepository mockery;

		[SetUp]
		public virtual void SetUp()
		{
			mockery = new MockRepository();
			
		}

		[TearDown]
		public virtual void TearDown()
		{

		}

		protected override IMockRequest BuildRequest()
		{
			StubRequest request = new StubRequest(Cookies);
			request.UrlReferrer = Referrer;

			return request;
		}


		protected override IMockResponse BuildResponse(UrlInfo info)
		{
			StubResponse response = new StubResponse(info,
											new DefaultUrlBuilder(),
											new StubServerUtility(),
											new RouteMatch(),
											Referrer);
			return response;
		}
	}
}