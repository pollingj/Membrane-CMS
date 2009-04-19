using Membrane.Commons.FormGeneration.Interfaces;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.Services;
using Membrane.Plugins.News.DTOs;
using Membrane.Plugins.News.Entities;

namespace Membrane.Plugins.News.Controllers
{
	public class NewsController : CRUDController<NewsArticleDTO, NewsArticle>
	{
		public NewsController(ICRUDService<NewsArticleDTO, NewsArticle> service, IPropertyReaderService<NewsArticleDTO> propertyReaderService)
			: base(service, propertyReaderService)
		{
		}
	}
}
