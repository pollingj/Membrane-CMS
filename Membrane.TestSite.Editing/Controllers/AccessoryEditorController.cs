using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.TestSite.Editing.Entities;

namespace Membrane.TestSite.Editing.Controllers
{
	public class AccessoryEditorController : BaseCrudController<Accessory>
	{
		private readonly IBaseCrudService<Product> productService;

		public AccessoryEditorController(IBaseCrudService<Accessory> service, IBaseCrudService<Product> productService)
			: base(service)
		{
			this.productService = productService;
		}

		// Would be nice to automate this
		public override void LoadSupportiveData()
		{
			PropertyBag["support.products"] = productService.GetAllData();
		}

	}
}