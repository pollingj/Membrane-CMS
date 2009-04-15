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
using Castle.MonoRail.Framework.Helpers.ValidationStrategy;
using Castle.MonoRail.Framework.JSGeneration;
using Castle.MonoRail.Framework.JSGeneration.jQuery;
using Castle.MonoRail.Framework.Routing;
using Castle.MonoRail.Views.Spark;
using Castle.MonoRail.WindsorExtension;
using Castle.Windsor;
using FluentNHibernate.AutoMap;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.NHibernate;
using NHibernate;
using Spark;
using Spark.FileSystem;
using Configuration=NHibernate.Cfg.Configuration;

namespace Membrane.Commons.Web.MonoRail
{
	public abstract class MonoRailNHibernateHttpApplication : HttpApplication, IContainerAccessor, IMonoRailConfigurationEvents
	{
		private readonly Assembly entitiesAssembly;
		protected static IWindsorContainer container;
		protected static List<Assembly> pluginAssemblies = new List<Assembly>();
		protected Assembly webAppAssembly;
		private Dictionary<string, Assembly> _assemblyList;

		private string pluginFolder;

		protected MonoRailNHibernateHttpApplication(Assembly entitiesAssembly)
		{
			this.entitiesAssembly = entitiesAssembly;
			webAppAssembly = Assembly.GetCallingAssembly();
		}

		public void Application_OnStart()
		{
			container = new WindsorContainer();
			pluginFolder = ConfigurationManager.AppSettings["plugins.path"];

			if (Directory.Exists(pluginFolder))
				RegisterPlugins();

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

		protected virtual void RegisterPlugins()
		{
			var pluginFilePaths = Directory.GetFiles(pluginFolder, "*.dll");

			foreach (var pluginFilePath in pluginFilePaths)
			{
				var pluginAssembly = getAssembly(pluginFilePath);

				if (!pluginAssembly.FullName.Contains("Membrane.Commons"))
				{
					try
					{
						var pluginTypes = pluginAssembly.GetTypes().Where(t => typeof (IMembranePlugin).IsAssignableFrom(t)).ToList();

						if (pluginTypes.Count > 0)
						{
							pluginAssemblies.Add(pluginAssembly);

							foreach (var pluginType in pluginTypes)
							{
								var plugin = (IMembranePlugin) Activator.CreateInstance(pluginType);

								plugin.Initialize();
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
			
			RegisterEntitiesAssembly(configuration, entitiesAssembly);

			foreach (Assembly pluginAssembly in pluginAssemblies)
			{
				RegisterEntitiesAssembly(configuration, pluginAssembly);
			}

			
			ISessionFactory sessionFactory = configuration.BuildSessionFactory();
			container.Kernel.AddComponentInstance("ISessionFactory", typeof(ISessionFactory), sessionFactory);
		}

		private void RegisterEntitiesAssembly(Configuration configuration, Assembly assembly)
		{
			var model = new AutoPersistenceModel
			{
				Conventions =
				{
					GetPrimaryKeyName = (type => "Id"),
					GetForeignKeyNameOfParent = (type => type.Name + "_Id"),
					GetForeignKeyName = (info => info.Name + "_Id"),
					GetTableName = (type => type.Name),
					GetManyToManyTableName = ((child, parent) => child.Name + "_To_" + parent.Name),
					//IsBaseType = (type => type == typeof(BaseModel) || type == typeof(BaseOrderModel))
				}
			};

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

		public abstract void RegisterApplicationComponents();
		public abstract void RegisterRoutes(RoutingEngine rules);

	}


}