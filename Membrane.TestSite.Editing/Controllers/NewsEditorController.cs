using System;
using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.TestSite.Editing.Entities;

namespace Membrane.TestSite.Editing.Controllers
{
	public class NewsEditorController : BaseCrudController<NewsArticle>
	{
		private IBaseCrudService<NewsType> newsTypeService;

		public NewsEditorController(IBaseCrudService<NewsArticle> service, IBaseCrudService<NewsType> newsTypeService)
			: base(service)
		{
			this.newsTypeService = newsTypeService;
		}

		// Would be nice to automate this
		public override void LoadSupportiveData()
		{
			PropertyBag["support.NewsType"] = newsTypeService.GetAllData();
		}
	}
}