using System;
using System.Reflection;
using System.Web;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
using FluentNHibernate;
using FluentNHibernate.AutoMap;
using FluentNHibernate.Cfg;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Membrane.Commons.Web.MonoRail
{
	public abstract class MonoRailNHibernateHttpApplication : HttpApplication, IContainerAccessor
	{
		protected static IWindsorContainer container;
		private readonly Assembly entitiesAssembly;
		private readonly Assembly controllersAssembly;

		protected MonoRailNHibernateHttpApplication(Assembly entitiesAssembly)
		{
			GuardAgainst.ArgumentNull(entitiesAssembly, "entitiesAssembly");

			controllersAssembly = Assembly.GetCallingAssembly();
			this.entitiesAssembly = entitiesAssembly;
		}

		public void Application_OnStart()
		{
			container = new WindsorContainer();

			RegisterRoutes(RoutingModuleEx.Engine);
			RegisterFacilities();
			RegisterComponents();
			RegisterControllers();

			ConfigureNHibernate();
		}

		public void Application_OnEnd()
		{
			container.Dispose();
		}

		public IWindsorContainer Container
		{
			get { return container; }
		}

		protected virtual void RegisterControllers()
		{

			container
				.Register(
				AllTypes.Pick().FromAssembly(controllersAssembly)
					.Configure(c => c.LifeStyle.Transient)
					.If(c => c.Name.Contains("Controller")))
				.Register(
				AllTypes.Pick().FromAssembly(Assembly.LoadFile(@"C:\Membrane\Membrane.TestSite.Editing\bin\Debug\Membrane.TestSite.Editing.dll"))
					.Configure(c => c.LifeStyle.Transient)
					.If(c => c.Name.Contains("Controller"))

				);
		}

		protected virtual void RegisterComponents()
		{
			container
				.AddComponent<IHttpContext, HttpContextFacade>()
				.AddComponent<INHibernateSessionLocater, NHibernateHttpContextSessionLocater>()
				.AddComponent("repository", typeof(IRepository<>), typeof(NHibernateRepository<>));

			RegisterApplicationComponents();
		}

		protected virtual void RegisterFacilities()
		{
			container
				.AddFacility("logging.facility", new LoggingFacility(LoggerImplementation.Log4net, "Logging.config"))
				.AddFacility<MonoRailFacility>()
				.AddFacility<NHibernateTransactionFacility>();
		}

		protected virtual void ConfigureNHibernate()
		{
			var configuration = new Configuration();

			var model = new AutoPersistenceModel
			{
				Conventions =
				{
					GetForeignKeyNameOfParent = (type => type.Name + "_Id"),
					GetForeignKeyName = (info => info.Name + "_Id"),
					GetTableName = (type => type.Name),
					GetManyToManyTableName = ((child, parent) => child.Name + "_To_" + parent.Name)
				}
			};

			model
				.AddEntityAssembly(entitiesAssembly)
				.Where(entity => entity.IsAbstract == false &&
								 entity.GetInterface("IEntity") != null)
				.Configure(configuration);

			configuration.AddAutoMappings(model);
			
			var sessionFactory = Fluently.Configure()  
				.Mappings(m => m.AutoMappings.Add(model))
				/*.ExposeConfiguration(cfg => new SchemaExport(cfg)  
				                            	.Create(false, true))*/
				.BuildSessionFactory();  

			container.Kernel.AddComponentInstance("ISessionFactory", typeof(ISessionFactory), sessionFactory);
		}

		/*Assembly currentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			//This handler is called only when the common language runtime tries to bind to the assembly and fails.

			//Retrieve the list of referenced assemblies in an array of AssemblyName.
			var strTempAssmbPath = "";

			var objExecutingAssemblies = Assembly.GetExecutingAssembly();
			var arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

			//Loop through the array of referenced assembly names.
			foreach (var strAssmbName in arrReferencedAssmbNames)
			{
				//Check for the assembly names that have raised the "AssemblyResolve" event.
				if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
				{
					//Build the path of the assembly from where it has to be loaded.				
					strTempAssmbPath = "C:\\Myassemblies\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
					break;
				}

			}
			//Load the assembly from the specified path. 					
			var MyAssembly = Assembly.LoadFrom(strTempAssmbPath);

			//Return the loaded assembly.
			return MyAssembly;		
		}*/


		public abstract void RegisterApplicationComponents();
		public abstract void RegisterRoutes(RoutingEngine rules);
	}

}