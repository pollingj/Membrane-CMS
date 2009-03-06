using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.TestSite.Editing.Entities;

namespace Membrane.TestSite.Editing.Controllers
{
	public class ProductEditorController : BaseCrudController<NewsArticle>
	{
		public ProductEditorController(IBaseCrudService<NewsArticle> service)
			: base(service)
		{
		}
	}
}