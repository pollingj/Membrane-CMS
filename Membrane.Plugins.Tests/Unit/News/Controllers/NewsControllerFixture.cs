using System;
using Membrane.Plugins.News.Controllers;
using Membrane.Plugins.News.DTOs;
using Membrane.Plugins.News.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Controllers;
using NUnit.Framework;

namespace Membrane.Plugins.Tests.Unit.News.Controllers
{
	public class NewsControllerFixture : CRUDControllerFixture<NewsArticleDTO, NewsArticle>
	{
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			NewDTO = new NewsArticleDTO { Name = "New Site Launch", Story = "<p>We now have a new website</p>", CreationDate = DateTime.Now };
			EditDTO = new NewsArticleDTO { Id = Guid.NewGuid(), Name = "New Site Launch Edited", Story = "<p>We now have a new website edited</p>", CreationDate = DateTime.Now };
			InvalidDTO = new NewsArticleDTO { Id = Guid.NewGuid() };
			DeleteDTO = new NewsArticleDTO { Id = Guid.NewGuid(), Name = "New Site Launch Deleted", Story = "<p>We now have a new website deleted</p>", CreationDate = DateTime.Now };


			Controller = new NewsController(Service, PropertyReaderService);

			PrepareController(Controller);
		}
	}
}