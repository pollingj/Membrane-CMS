using System;
using System.Configuration;
using System.Reflection;
using AutoMapper;
using Castle.Core;
using Castle.Windsor;
using Membrane.Commons;
using Membrane.Plugins.News.Controllers;
using Membrane.Plugins.News.DTOs;
using Membrane.Plugins.News.Entities;
using Migrator.Framework;

namespace Membrane.Plugins.News
{
	public class Plugin : IMembranePlugin
	{
		public string Name
		{
			get { return "News"; }
		}

		public string Version
		{
			get { return "1.0.0"; }
		}


		public void Initialize()
		{
			Mapper.CreateMap<NewsArticleDTO, NewsArticle>();
			Mapper.CreateMap<NewsArticle, NewsArticleDTO>();
		}

		public void RegisterComponents(IWindsorContainer container)
		{
			container.AddComponentLifeStyle("newscontroller", typeof(NewsController), LifestyleType.Transient);
		}

		public void RemoveComponents(IWindsorContainer container)
		{
			container.Kernel.RemoveComponent("newscontroller");
		}

		public void Install()
		{
			// Run sql install scripts
			var migrator = GetMigrator();

			migrator.MigrateToLastVersion();

		}

		public void Uninstall()
		{
			// Run sql uninstall scripts
			var migrator = GetMigrator();
			migrator.MigrateTo(0);

		}

		public void Upgrade()
		{
			throw new NotImplementedException();
		}

		private Migrator.Migrator GetMigrator()
		{
			var asm = Assembly.GetCallingAssembly();

			Migrator.Migrator migrator = new Migrator.Migrator("SqlServer", ConfigurationManager.AppSettings["conString"], asm, false);

			return migrator;
		}
	}
}