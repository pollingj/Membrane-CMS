using System;
using Membrane.Core.Services.Interfaces;

namespace Membrane.Controllers
{
	public class ContentController : BaseController
	{
		private readonly IContentService service;

		public ContentController(IContentService service)
		{
			this.service = service;
		}

		public void List(string contentType)
		{
			var modelName = service.GetContentModelName(contentType);
			PropertyBag["ListItems"] = service.GetCurrentElements(modelName);
		}

		public void Edit(string contentType, int id)
		{
			throw new NotImplementedException();
		}
	}
}