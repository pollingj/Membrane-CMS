using Membrane.Commons.Editors;
using Membrane.Commons.Services;
using Membrane.TestSite.Editing.Entities;

namespace Membrane.TestSite.Editing.Controllers
{
	public class ProductEditorController : BaseCrudController<Product>
	{
		private readonly IBaseCrudService<Accessory> accessoryService;

		public ProductEditorController(IBaseCrudService<Product> productService, IBaseCrudService<Accessory> accessoryService)
			: base(productService)
		{
			this.accessoryService = accessoryService;
		}

		public override void LoadSupportiveData()
		{
			accessoryService.GetAllData();
		}

	}
}