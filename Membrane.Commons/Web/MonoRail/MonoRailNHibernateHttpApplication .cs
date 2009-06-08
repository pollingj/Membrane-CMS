using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Configuration;
using Castle.MonoRail.Framework.Helpers.ValidationStrategy;
using Castle.MonoRail.Framework.JSGeneration;
using Castle.MonoRail.Framework.JSGeneration.jQuery;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
using FluentNHibernate.AutoMap;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.Conventions;
using Membrane.Commons.Persistence.NHibernate;
using Membrane.Commons.Plugin.Entities;
using NHibernate;
using Configuration=NHibernate.Cfg.Configuration;

namespace Membrane.Commons.Web.MonoRail
{
	public abstract class MonoRailNHibernateHttpApplication : HttpApplication, IContainerAccessor, IMonoRailConfigurationEvents
	{
		private readonly Assembly[] entitiesAssemblies;
		protected static IWindsorContainer container;
		protected static List<Assembly> pluginAssemblies = new List<Assembly>();
		protected Assembly webAppAssembly;
		private Dictionary<string, Assembly> _assemblyList;

		private string pluginFolder;

		protected MonoRailNHibernateHttpApplication(Assembly[] entitiesAssemblies)
		{
			this.entitiesAssemblies = entitiesAssemblies;
			webAppAssembly = Assembly.GetCallingAssembly();
		}

		public void Application_OnStart()
		{
			container = new WindsorContainer();
			pluginFolder = ConfigurationManager.AppSettings["plugins.path"];

			RegisterRoutes(RoutingModuleEx.Engine);

			RegisterFacilities();
			RegisterComponents();
			RegisterControllers();

			if (Directory.Exists(pluginFolder))
				ResolveEntityPluginDlls();

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

			// Get MonoRail to use jQuery
			configuration.JSGeneratorConfiguration.AddLibrary("jquery-1.2.6", typeof(JQueryGenerator))
				.AddExtension(typeof(CommonJSExtension))
				.BrowserValidatorIs(typeof(JQueryValidator))
				.SetAsDefault();
		}

		protected virtual void RegisterControllers()
		{
			RegisterAssemblyControllers(webAppAssembly);
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

			foreach (var entityAssembly in entitiesAssemblies)
			{
				RegisterEntitiesAssembly(configuration, entityAssembly);
			}
			

			/*foreach (Assembly pluginAssembly in pluginAssemblies)
			{
				RegisterEntitiesAssembly(configuration, pluginAssembly);
			}*/


			ISessionFactory sessionFactory = configuration.BuildSessionFactory();
			container.Kernel.AddComponentInstance("ISessionFactory", typeof(ISessionFactory), sessionFactory);
		}

		private void RegisterEntitiesAssembly(Configuration configuration, Assembly assembly)
		{
			var model = new AutoPersistenceModel()
				.WithSetup(s => s.IsBaseType = (type => type == typeof (BaseEntity) || type == typeof (BaseOrderedEntity)
				                                        || type == typeof (BaseVersionedEntity) || type == typeof (BaseVersionedAndOrderedEntity)))
				.ConventionDiscovery.Setup(c =>
				                           	{
				                           		c.Add<FluentNHibernate.Conventions.Defaults.PrimaryKeyConvention>();
				                           		c.Add<ForeignKeyConvention>();
				                           		c.Add<TableNameConvention>();
				                           		c.Add<ManyToManyTableConvention>();
				                           	}
				);

			model
				.AddEntityAssembly(assembly)
				.Where(entity => entity.IsAbstract == false &&
				                 entity.GetInterface("IEntity") != null)
				.Configure(configuration);
			

		}

		/// <summary>
		/// Puts all the plugin dlls into a dictionary object for resolving later 
		/// </summary>
		private void ResolveEntityPluginDlls()
		{
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
			var dlls = Directory.GetFiles(ConfigurationManager.AppSettings["plugins.path"], "*.dll");
			_assemblyList = new Dictionary<string, Assembly>(dlls.Length);
			foreach (var fileName in dlls)
			{
				var asm = getAssembly(fileName);
	
				_assemblyList.Add(asm.FullName.Split(',')[0], asm);
			}
		}

		private Assembly getAssembly(string fileName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Assembly foundAssembly = null;
			foreach (Assembly assembly in assemblies)
			{
				if (assembly.FullName == AssemblyName.GetAssemblyName(fileName).FullName)
				{
					foundAssembly = assembly;
					break;
				}
			}

			if (foundAssembly == null)
			{
				var assemblyBytes = File.ReadAllBytes(fileName);
				foundAssembly = Assembly.Load(assemblyBytes);
			}

			return foundAssembly;
		}

		/// <summary>
		/// Resolves the dependancies when RegisterEntitiesAssembly is called for the plugin entities
		/// It searches the assemblyList stored earlier
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		/// <returns>The resolved Assembly</returns>
		Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			Assembly assembly = null;

			var name = args.Name;

			// Make sure the Name is clean to match the dictionary key
			if (name.IndexOf(',') > -1)
				name = name.Remove(name.IndexOf(','));

			if (_assemblyList.ContainsKey(name))
				assembly = _assemblyList[name];

			return assembly;

		}

		protected abstract void RegisterApplicationComponents();
		protected abstract void RegisterRoutes(RoutingEngine rules);
	}


}