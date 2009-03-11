using System.Reflection;
using System.Web;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
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
		protected Configuration configuration;

		protected MonoRailNHibernateHttpApplication(Assembly entitiesAssembly)
		{
			GuardAgainst.ArgumentNull(entitiesAssembly, "entitiesAssembly");

			controllersAssembly = Assembly.GetCallingAssembly();
			this.entitiesAssembly = entitiesAssembly;
		}

		public void Application_OnStart()
		{
			container = new WindsorContainer();
			configuration = new Configuration();

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
					.If(c => c.Name.Contains("Controller")));
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
			
			var sessionFactory = Fluently.Configure()  
				.Mappings(m => m.AutoMappings.Add(model))
				/*.ExposeConfiguration(cfg => new SchemaExport(cfg)  
				                            	.Create(false, true))*/
				.BuildSessionFactory();  

			container.Kernel.AddComponentInstance("ISessionFactory", typeof(ISessionFactory), sessionFactory);
		}



		public abstract void RegisterApplicationComponents();
		public abstract void RegisterRoutes(RoutingEngine rules);
	}

}