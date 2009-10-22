using System;
using System.Collections.Generic;
using Castle.MonoRail.Framework;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Core.Services.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;

namespace Membrane.Controllers.User
{
	public class PageController : CRUDController<PageDTO, Page>
	{
		private readonly IPageService service;

		public PageController(IPageService service, IPropertyReaderService<PageDTO> propertyReaderService) : base(service, propertyReaderService)
		{
			this.service = service;
		}

		public void AddNewContentBlock(Guid pageId)
		{
			PropertyBag["CurrentPageID"] = pageId;
		}

		public void SubmitContentBlock(Guid pageId, [DataBind("contentblock", Validate = true)]ContentBlockDTO block)
		{
			var submitError = false;
			if (Validator.IsValid(block))
			{
				var newId = service.AddNewContentBlockToPage(pageId, block);
				if (newId == Guid.Empty)
				{
					submitError = CreateError("There was a problem inserting this item.");
				}
			}
			else
			{
				Flash["error"] = Validator.GetErrorSummary(block);
				submitError = true;
			}

			if (submitError)
			{
				RedirectToReferrer();
			}
			else
			{
				RedirectToAction("Edit", new Dictionary<string, Guid> { { "Id", pageId } });
			}
		}

		public void EditContentBlock(Guid blockId)
		{
			PropertyBag["contentBlock"] = service.GetContentBlock(blockId);
		}
	}
}