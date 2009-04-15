using Membrane.Commons.CRUD.Controllers;
using Membrane.Commons.CRUD.Services;
using Membrane.Commons.FormGeneration.Interfaces;
using Membrane.Plugins.DTOs;
using Membrane.Plugins.Entities;

namespace Membrane.Plugins.Controllers
{
	public class NewsController : CRUDController<NewsArticleDTO, NewsArticle>
	{
		public NewsController(ICRUDService<NewsArticleDTO, NewsArticle> service, IAutoGenerator<NewsArticleDTO> autoGenerator) : base(service, autoGenerator)
		{
		}
	}
}
