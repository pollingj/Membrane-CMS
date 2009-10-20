using System;
using Membrane.Controllers.User;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Controllers;
using NUnit.Framework;

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
			NewDTO = new PageDTO { Name = "About Us", Content = "<p>This is the about us page</p>", Template = template };
			EditDTO = new PageDTO { Id = Guid.NewGuid(), Name = "About Us Edited", Content = "<p>This is the about us page edited</p>", Template = template };
			InvalidDTO = new PageDTO { Id = Guid.NewGuid() };
			DeleteDTO = new PageDTO { Id = Guid.NewGuid(), Name = "About Us Deleted", Content = "<p>This is the about us page deleted</p>", Template = template };


			Controller = new PageController(Service, PropertyReaderService);

			PrepareController(Controller);
		}
	}
}