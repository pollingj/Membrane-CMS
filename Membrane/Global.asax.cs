using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
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