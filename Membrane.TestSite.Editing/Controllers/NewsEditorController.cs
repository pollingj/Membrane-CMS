using System;
using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.TestSite.Editing.Entities;

namespace Membrane.TestSite.Editing.Controllers
{
	public class NewsEditorController : BaseCrudController<NewsArticle>
	{
		public NewsEditorController(IBaseCrudService<NewsArticle> service)
			: base(service)
		{
		}

		public override void List()
		{
			var newArticle = new NewsArticle {Id = new Guid(), Headline = "New Headline", Story = "New Story"};
			service.Create(newArticle);
			base.List();
			PropertyBag["singleItem"] = service.GetItem(new Guid("f61c3d2c-e2b2-4b1d-9d3d-9bd100879aa3"));
		}
	}
}