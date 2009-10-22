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
		private IPageService service;
		private PageController controller;

		private Guid pageId;
		private ContentBlockDTO contentBlock, edittedBlock;

		[SetUp]
		public void SetUp()
		{
			base.SetUp();
			service = mockery.DynamicMock<IPageService>();
			controller = new PageController(service, PropertyReaderService);
			SetUp(controller, service);

			pageId = Guid.NewGuid();
			contentBlock = new ContentBlockDTO { Name = "Sidebar" };
			edittedBlock = new ContentBlockDTO {Id = Guid.NewGuid(), Name = "Sidebar", Content = "<p>Sidebar content here</p>"};

			var template = new TemplateDTO {Name = "Main Template", Content = "<p>This is the main template</p>"};
			var contentBlocks = new List<ContentBlockDTO>
			                    	{
			                    		new ContentBlockDTO {Name = "Body", Content = "<p>Body Content here</p>"}
			                    	};
			NewDTO = new PageDTO {Name = "About Us", Template = template, ContentBlocks = contentBlocks };
			EditDTO = new PageDTO { Id = Guid.NewGuid(), Name = "About Us Edited", Template = template, ContentBlocks = contentBlocks };
			InvalidDTO = new PageDTO { Id = Guid.NewGuid() };
			DeleteDTO = new PageDTO { Id = Guid.NewGuid(), Name = "About Us Deleted", Template = template, ContentBlocks = contentBlocks };



			PrepareController(controller);
		}

		[Test]
		public void CanShowNewContentBlockForm()
		{
			var pageid = Guid.NewGuid();
			controller.AddNewContentBlock(pageid);

			Assert.AreEqual(pageid, controller.PropertyBag["CurrentPageID"]);
		}

		[Test]
		public void CanSuccessfullyCreateNewContentBlock()
		{
			submitValidBlock(Guid.NewGuid());

			Assert.IsNull(controller.Flash["error"]);
			Assert.AreEqual(string.Format("/Controller/Edit.castle?Id={0}", pageId), Response.RedirectedTo);
		}

		[Test]
		public void CanFailValidationCreatingNewContentBlock()
		{
			controller.SubmitContentBlock(pageId, new ContentBlockDTO { Name = "" });

			assertFailedBlockSubmission();
		}

		[Test]
		public void CanFailCreateNewContentBlock()
		{
			submitValidBlock(Guid.Empty);

			assertFailedBlockSubmission();
		}

		[Test]
		public void CanSuccessfullyDisplayEditContentBlock()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetContentBlock(edittedBlock.Id)).Return(edittedBlock))
				.Verify(() => controller.EditContentBlock(edittedBlock.Id));

			Assert.AreEqual(edittedBlock, controller.PropertyBag["contentBlock"]);
			Assert.AreEqual(@"Controller\Action", Controller.SelectedViewName);
		}

		private void assertFailedBlockSubmission()
		{
			Assert.IsNotNull(controller.Flash["error"]);
			Assert.AreEqual(@"Controller\Action", Controller.SelectedViewName);
		}

		private void submitValidBlock(Guid id)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.AddNewContentBlockToPage(pageId, contentBlock)).Return(id))
				.Verify(() => controller.SubmitContentBlock(pageId, contentBlock));
		}
	}
}