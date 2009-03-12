using Castle.Components.Validator;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.Framework.Services;
using Castle.MonoRail.Framework.Test;
using Castle.MonoRail.TestSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Test.Controllers
{
	public class BaseControllerFixture : BaseControllerTest
	{
		public MockRepository mockery;
		private const string referrer = "http://www.example.com";

		protected override IMockRequest BuildRequest()
		{
			var request = new StubRequest(Cookies) {UrlReferrer = referrer};

			return request;
		}

		protected override IMockResponse BuildResponse(UrlInfo info)
		{
			var response = new StubResponse(info,
											new DefaultUrlBuilder(),
											new StubServerUtility(),
											new RouteMatch(),
											referrer);
			return response;
		}

		[SetUp]
		public virtual void SetUp()
		{
			mockery = new MockRepository();
		}

		/// <summary>
		/// Method to simulate a controller validation error
		/// </summary>
		/// <param name="controller">The controller</param>
		/// <param name="instance">The item that is failing the validation</param>
		public void SimulateOneValidationErrorFor(SmartDispatcherController controller, object instance)
		{
			controller.PopulateValidatorErrorSummary(instance, CreateDummyErrorSummaryWithOneError());
		}

		private ErrorSummary CreateDummyErrorSummaryWithOneError()
		{
			var errors = new ErrorSummary();
			errors.RegisterErrorMessage("blah", "blah");

			return errors;
		}
	}


}