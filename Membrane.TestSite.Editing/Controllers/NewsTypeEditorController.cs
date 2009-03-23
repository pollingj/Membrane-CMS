using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.TestSite.Editing.Entities;

namespace Membrane.TestSite.Editing.Controllers
{
	public class NewsTypeEditorController : BaseCrudController<NewsType>
	{
		public NewsTypeEditorController(IBaseCrudService<NewsType> service)
			: base(service)
		{
		}
	}
}