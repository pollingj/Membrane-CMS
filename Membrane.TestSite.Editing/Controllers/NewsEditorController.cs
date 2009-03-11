using System.ComponentModel.Composition;
using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.TestSite.Editing.Entities;

namespace Membrane.TestSite.Editing.Controllers
{
	[Export(typeof(IEditorController<>))]
	public class NewsEditorController : BaseCrudController<NewsArticle>
	{
		[ImportingConstructor]
		public NewsEditorController(IBaseCrudService<NewsArticle> service)
			: base(service)
		{
		}
	}
}