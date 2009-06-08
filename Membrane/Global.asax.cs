using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.Framework.Services;
using Castle.MonoRail.Framework.ViewComponents;
using Castle.Windsor;
using Membrane.Commons.FormGeneration.Services;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.Services;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Commons.Web.MonoRail;
using Membrane.Controllers;
using Membrane.Core.Mappers;
using Membrane.ViewComponents;

namespace Membrane
{
	/// <summary>
	///
	/// </summary>
	public class Global : MonoRailNHibernateHttpApplication
    {
		public Global()
			: base(new [] { Assembly.Load("Membrane.Entities"), Assembly.Load("Membrane.Commons") })
		{
			AutoMapperConfiguration.Configure();
		}

		protected override void RegisterApplicationComponents()
		{
			container.Register(
				AllTypes.Pick().FromAssembly(Assembly.Load("Membrane.Commons"))
				.Configure(c => c.LifeStyle.Transient).If(c => c.Namespace.Contains("Wrappers"))
				.WithService.FirstInterface());

			container.Register(
				AllTypes.Pick().FromAssembly(Assembly.Load("Membrane.Core"))
					.Configure(c => c.LifeStyle.Transient).If(c => c.Name.Contains("Service"))
					.WithService.FirstInterface());

			
			container.AddComponent("autogenerator", typeof(IPropertyReaderService<>), typeof(PropertyReaderService<>));
			container.AddComponent("crudservice", typeof(ICRUDService<,>), typeof(CRUDService<,>));
			container.AddComponent("ordercrudservice", typeof(IOrderCRUDService<,>), typeof(OrderCRUDService<,>));
			container.AddComponent("FormGenerator", typeof(AutomaticFormFieldGeneratorComponent));

			container.AddComponent<IScriptBuilder, YuiScriptBuilder>();
			container.AddComponent("JSCombine", typeof(CombineJSViewComponent));

			container.Register(Component.For<IWindsorContainer>().Instance(container));
		}

		protected override void RegisterRoutes(RoutingEngine rules)
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