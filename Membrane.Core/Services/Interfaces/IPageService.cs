using System;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Entities;

namespace Membrane.Core.Services.Interfaces
{
	public interface IPageService : ICRUDService<PageDTO, Page>
	{
		Guid AddNewContentBlockToPage(Guid pageId, ContentBlockDTO block);
		bool UpdateContentBlock(ContentBlockDTO block);
		ContentBlockDTO GetContentBlock(Guid guid);
	}
}