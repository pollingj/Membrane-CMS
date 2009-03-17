using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Configuration;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
using FluentNHibernate.AutoMap;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.NHibernate;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Configuration=NHibernate.Cfg.Configuration;

namespace Membrane.Commons.Web.MonoRail
{
	public abstract class MonoRailNHibernateHttpApplication : HttpApplication, IContainerAccessor, IMonoRailConfigurationEvents
	{
		private readonly Assembly entitiesAssembly;
		protected static IWindsorContainer container;
		protected static List<Assembly> pluginAssemblies = new List<Assembly>();
		protected Assembly webAppAssembly;

		protected MonoRailNHibernateHttpApplication(Assembly entitiesAssembly)
		{
			this.entitiesAssembly = entitiesAssembly;
			webAppAssembly = Assembly.GetCallingAssembly();
		}

		public void Application_OnStart()
		{
			container = new WindsorContainer();

			RegisterPlugins();

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

		protected virtual void RegisterPlugins()
		{
			var pluginFolder = ConfigurationManager.AppSettings["plugins.path"];
			var pluginFilePaths = Directory.GetFiles(pluginFolder, "*.dll");

			foreach (var pluginFilePath in pluginFilePaths)
			{
				var pluginAssembly = Assembly.LoadFile(pluginFilePath);

				try
				{
					var pluginTypes = pluginAssembly.GetTypes().Where(t => typeof(IWindsorPlugin).IsAssignableFrom(t)).ToList();

					if (pluginTypes.Count > 0)
					{
						pluginAssemblies.Add(pluginAssembly);

						foreach (var pluginType in pluginTypes)
						{
							var plugin = (IWindsorPlugin)Activator.CreateInstance(pluginType);
							plugin.RegisterComponents(container);
						}
					}
				}
				catch (ReflectionTypeLoadException)
				{
					//There was a reflection error, ignore for now but probably need to at least log this info
				}
			}
		}

		public void Configure(IMonoRailConfiguration configuration)
		{
			foreach (var pluginAssembly in pluginAssemblies)
			{
				var assemblyName = pluginAssembly.GetName().Name;
				try
				{
					configuration.ViewEngineConfig.AssemblySources.Add(new AssemblySourceInfo(assemblyName, assemblyName + ".Views"));
				}
				catch(Exception)
				{
					
				}
			}
		}

		protected virtual void RegisterControllers()
		{
			RegisterAssemblyControllers(webAppAssembly);

			foreach (var pluginAssembly in pluginAssemblies)
			{
				RegisterAssemblyControllers(pluginAssembly);
			}
		}

		private void RegisterAssemblyControllers(Assembly assembly)
		{
			container
				.Register(
				AllTypes.Pick().FromAssembly(assembly)
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

			foreach (var pluginAssembly in pluginAssemblies)
			{
				model.AddEntityAssembly(pluginAssembly);
			}

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