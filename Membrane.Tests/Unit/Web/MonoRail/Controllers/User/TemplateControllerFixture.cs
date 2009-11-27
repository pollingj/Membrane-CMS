using System;
using Membrane.Controllers.User;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.Tests.Unit.Commons.Plugin.Controllers;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.User
{
	[TestFixture]
	public class TemplateControllerFixture: CRUDControllerFixture<TemplateDTO, Template>
	{
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			NewDTO = new TemplateDTO { Name = "Main Template", Content = "<p>This is a template</p>" };
			EditDTO = new TemplateDTO { Id = Guid.NewGuid(), Name = "Main Template Edited", Content = "<p>This is a template edited</p>" };
			InvalidDTO = new TemplateDTO { Id = Guid.NewGuid() };
			DeleteDTO = new TemplateDTO { Id = Guid.NewGuid(), Name = "Main Template Deleted", Content = "<p>This is a template deleted</p>" };


			Controller = new TemplateController(Service, PropertyReaderService);

			PrepareController(Controller);
		}
	}
}