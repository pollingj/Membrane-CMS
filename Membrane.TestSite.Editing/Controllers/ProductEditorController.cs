using System.ComponentModel.Composition;
using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.TestSite.Editing.Entities;

namespace Membrane.TestSite.Editing.Controllers
{
	[Export(typeof(IEditorController<>))]
	public class ProductEditorController : BaseCrudController<Product>
	{
		[ImportingConstructor]
		public ProductEditorController(IBaseCrudService<Product> service)
			: base(service)
		{
		}
		/*public ProductEditorController(IBaseCrudService<Product> service) : base(service)
		{
		}*/
	}
}