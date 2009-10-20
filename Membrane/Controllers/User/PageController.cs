using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;

namespace Membrane.Controllers.User
{
	public class PageController : CRUDController<PageDTO, Page>
	{
		public PageController(ICRUDService<PageDTO, Page> service, IPropertyReaderService<PageDTO> propertyReaderService) : base(service, propertyReaderService)
		{
		}
	}
}