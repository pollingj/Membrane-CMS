using System;
using System.Collections.Generic;
using Membrane.Controllers.User;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.User
{
	[TestFixture]
	public class PageControllerFixture : CRUDControllerFixture<PageDTO, Page>
	{
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			var template = new TemplateDTO {Name = "Main Template", Content = "<p>This is the main template</p>"};
			var contentBlocks = new List<ContentBlockDTO>
			                    	{
			                    		new ContentBlockDTO {Name = "Body", Content = "<p>Body Content here</p>"}
			                    	};
			NewDTO = new PageDTO {Name = "About Us", Template = template, ContentBlocks = contentBlocks };
			EditDTO = new PageDTO { Id = Guid.NewGuid(), Name = "About Us Edited", Template = template, ContentBlocks = contentBlocks };
			InvalidDTO = new PageDTO { Id = Guid.NewGuid() };
			DeleteDTO = new PageDTO { Id = Guid.NewGuid(), Name = "About Us Deleted", Template = template, ContentBlocks = contentBlocks };

			Service = mockery.DynamicMock<IPageService>();

			Controller = new PageController(Service, PropertyReaderService);

			PrepareController(Controller);
		}

		/*[Test]
		public void CanCreateNewContentBlock()
		{
			With.Mocks(mockery)
				.Expecting(() => Service.AddNewContentBlockToPage(EditDTO, new ContentBlockDTO { Name = "Sidebar", Content = "<p>Sidebar content here</p>" }))
				.Verify(() => Controller.AddContentBlock());
				
			
		}*/
	}
}