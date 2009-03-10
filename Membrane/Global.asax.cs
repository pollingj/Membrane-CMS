using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MonoRail.Framework.Routing;
using Membrane.Commons.Persistence;
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
		
		//public IEnumerable<IEditorController> Editors { get; set; }
		[Import]
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

			
			var mefcontainer = new CompositionContainer(catalog);
			var batch = new CompositionBatch();
			batch.AddPart(this);
			mefcontainer.Compose(batch);


		}

		protected override void RegisterControllers()
		{
			base.RegisterControllers();

			foreach (var editor in Editors)
			{
				container.Register(AllTypes.From(editor.GetType()));
			}
		}

		/*protected override void ConfigureNHibernate()
		{
			
			foreach (var entity in Entities)
			{
				var type = entity.GetType();
				var methodInfo = model.GetType().GetMethod("AutoMap");
				var generic = methodInfo.MakeGenericMethod(type);
				generic.Invoke(model, null);



			}
			//model.AutoMap<>()

			base.ConfigureNHibernate();
		}*/

		public override void RegisterRoutes(RoutingEngine rules)
		{
			rules.Add(new PatternRoute("[controller]/[action]")
				.DefaultForArea().IsEmpty
				.DefaultForController().Is("Home")
				.DefaultForAction().Is("Index"));
		}

    }
}