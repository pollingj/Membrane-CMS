using System.Reflection;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.Framework.Services;
using Castle.MonoRail.Framework.ViewComponents;
using Castle.MonoRail.WindsorExtension;
using Membrane.Commons.FormGeneration.Services;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.Services;
using Membrane.Commons.Web.MonoRail;
using Membrane.Commons.Wrappers;
using Membrane.Commons.Wrappers.Interfaces;
using Membrane.Controllers;
using Membrane.Core.Mappers;
using Membrane.Core.Services;
using Membrane.Core.Services.Interfaces;
using Membrane.ViewComponents;

namespace Membrane
{
	/// <summary>
	/// TODO: Need to consider the url rewriting for Membrane
	/// </summary>
	public class Global : MonoRailNHibernateHttpApplication
    {
		public Global()
			: base(Assembly.Load("Membrane.Entities"))
		{
			AutoMapperConfiguration.Configure();
		}

		public override void RegisterApplicationComponents()
		{
			container.AddComponent<IFormsAuthentication, FormsAuthenticationWrapper>();
			container.AddComponent<IAuthenticationService, AuthenticationService>();
			container.AddComponent<IUserService, UserService>();
			container.AddComponent("autogenerator", typeof(IPropertyReaderService<>), typeof(PropertyReaderService<>));
			container.AddComponent("crudservice", typeof(ICRUDService<,>), typeof(CRUDService<,>));
			container.AddComponent("ordercrudservice", typeof(IOrderCRUDService<,>), typeof(OrderCRUDService<,>));
			container.AddComponent("FormGenerator", typeof(AutomaticFormFieldGeneratorComponent));

			container.AddComponent<IScriptBuilder, YuiScriptBuilder>();
			container.AddComponent("JSCombine", typeof(CombineJSViewComponent));
		}

		public override void RegisterRoutes(RoutingEngine rules)
		{
			rules.Add(new PatternRoute("/")
				.DefaultForArea().IsEmpty
				.DefaultForController().Is<LoginController>()
				.DefaultForAction().Is("index"));

			rules.Add(new PatternRoute("/<controller>/<action>")
						.DefaultForArea().IsEmpty
						.Restrict("controller").AnythingBut("MonoRail"));

			rules.Add(new PatternRoute("/<area>/<controller>/<action>")
				.Restrict("area").AnyOf("Administrator", "User"));


			
		}

    }
}