using System.Reflection;
using Castle.MonoRail.Framework.Routing;
using Membrane.Commons.Services;
using Membrane.Commons.Web.MonoRail;

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
		}

		public override void RegisterApplicationComponents()
		{
            container.AddComponent("basecrudservice", typeof(IBaseCrudService<>), typeof(BaseCrudService<>));		
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