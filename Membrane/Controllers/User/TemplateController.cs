using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;

namespace Membrane.Controllers.User
{
	public class TemplateController : CRUDController<TemplateDTO, Template>
	{
		public TemplateController(ICRUDService<TemplateDTO, Template> service, IPropertyReaderService<TemplateDTO> propertyReaderService) : base(service, propertyReaderService)
		{
		}
	}
}