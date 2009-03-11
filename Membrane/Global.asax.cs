using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework.Routing;
using Membrane.Commons.Editors;
using Membrane.Commons.MEFExportProvider;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.NHibernate;
using Membrane.Commons.Services;
using Membrane.Commons.Web.MonoRail;
using Membrane.Core.Services;
using Membrane.Core.Services.Interfaces;
using FluentNHibernate;
using FluentNHibernate.AutoMap;
using FluentNHibernate.Cfg;
using Membrane.Entities;

namespace Membrane
{
	/// <summary>
	/// TODO: Need to consider the url rewriting for Membrane
	/// </summary>
	public class Global : MonoRailNHibernateHttpApplication
    {
		//[Import]
		//public IEnumerable<IEditorController> Editors { get; set; }
		[Import(typeof(IEditorController<>))]
		public IEnumerable Editors { get; set; }

		/*[Import]
		public IEnumerable<IEntity> Entities { get; set; }*/



		public Global()
			: base(Assembly.Load("Membrane.Entities"))
		{
		}

		public override void RegisterApplicationComponents()
		{
			container.AddComponent<IContentService, ContentService>();
            container.AddComponent("basecrudservice", typeof(IBaseCrudService<>), typeof(BaseCrudService<>));

			// Try registering the MEF stuff here


			var catalog = new AggregateCatalog(new ComposablePartCatalog[] 
                { 
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()), 
                    new DirectoryCatalog(@"C:\Membrane\Membrane.TestSite.Editing\bin\Debug")
                });

			var factoryProvider = new FactoryExportProvider<IService>(GetService);
			var mefcontainer = new CompositionContainer(catalog, factoryProvider);
			var batch = new CompositionBatch();
			batch.AddPart(this);
			mefcontainer.Compose(batch);
		}

		public IService GetService(Type type) 
		{
			return null;
		}

		protected override void RegisterControllers()
		{
			base.RegisterControllers();

			foreach (var editor in Editors)
			{
				container.AddComponent(editor.GetType().Name, editor.GetType());//.Register(AllTypes.From(editor.GetType()));
			}
		}

		public override void RegisterRoutes(RoutingEngine rules)
		{
			rules.Add(new PatternRoute("[controller]/[action]")
				.DefaultForArea().IsEmpty
				.DefaultForController().Is("Home")
				.DefaultForAction().Is("Index"));
		}

    }
}