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

			NewDTO = new PageDTO { Name = "About Us", Content = "<p>This is the about us page</p>" };
			EditDTO = new PageDTO { Id = Guid.NewGuid(), Name = "About Us Edited", Content = "<p>This is the about us page edited</p>" };
			InvalidDTO = new PageDTO { Id = Guid.NewGuid() };
			DeleteDTO = new PageDTO { Id = Guid.NewGuid(), Name = "About Us Deleted", Content = "<p>This is the about us page deleted</p>" };


			Controller = new PageController(Service, PropertyReaderService);

			PrepareController(Controller);
		}
	}
}