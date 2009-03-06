using System.ComponentModel.Composition;
using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.TestSite.Editing.Entities;

namespace Membrane.TestSite.Editing.Controllers
{
	public class NewsEditorController : BaseCrudController<NewsArticle>
	{
		[Import]
		public NewsEditorController(IBaseCrudService<NewsArticle> service)
			: base(service)
		{
		}
	}
}