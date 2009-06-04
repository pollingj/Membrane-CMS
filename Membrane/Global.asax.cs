using System.Reflection;
using SystemWrapper;
using SystemWrapper.IO;
using SystemWrapper.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.Framework.Services;
using Castle.MonoRail.Framework.ViewComponents;
using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
using Membrane.Commons.FormGeneration.Services;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.Services;
using Membrane.Commons.Plugin.Services.Interfaces;
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
	///
	/// </summary>
	public class Global : MonoRailNHibernateHttpApplication
    {
		public Global()
			: base(new [] { Assembly.Load("Membrane.Entities"), Assembly.Load("Membrane.Commons") })
		{
			AutoMapperConfiguration.Configure();
		}

		public override void RegisterApplicationComponents()
		{
			container.AddComponent<IFormsAuthentication, FormsAuthenticationWrapper>();
			/*			container.AddComponent<IAuthenticationService, AuthenticationService>();
						container.AddComponent<IEncryptionService, EncryptionService>();
						container.AddComponent<IUserService, UserService>();
						container.AddComponent<IPluginsService, PluginsService>();*/
			container.AddComponent<IAssemblyWrap, AssemblyWrap>();
			container.AddComponent<IAppDomainWrap, AppDomainWrap>();
			container.AddComponent<IAssemblyNameWrap, AssemblyName>();
			container.AddComponent<IFileWrap, FileWrap>();
			container.AddComponent<IDirectoryWrap, DirectoryWrap>();

			var assembly = Assembly.Load("Membrane.Core");

			container
				.Register(
				AllTypes.Pick().FromAssembly(assembly)
					.Configure(c => c.LifeStyle.Transient)
					.If(c => c.Name.Contains("Service"))
				);

			
			container.AddComponent("autogenerator", typeof(IPropertyReaderService<>), typeof(PropertyReaderService<>));
			container.AddComponent("crudservice", typeof(ICRUDService<,>), typeof(CRUDService<,>));
			container.AddComponent("ordercrudservice", typeof(IOrderCRUDService<,>), typeof(OrderCRUDService<,>));
			container.AddComponent("FormGenerator", typeof(AutomaticFormFieldGeneratorComponent));

			container.AddComponent<IScriptBuilder, YuiScriptBuilder>();
			container.AddComponent("JSCombine", typeof(CombineJSViewComponent));

			container.AddComponent<IWindsorContainer, WindsorContainer>();
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