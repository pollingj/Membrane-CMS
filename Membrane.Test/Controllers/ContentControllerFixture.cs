using System.Collections.Generic;
using Membrane.Controllers;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Test.Controllers
{
	[TestFixture]
	public class ContentControllerFixture : BaseControllerFixture
	{
		private ContentController controller;
		private IContentService service;

		private const string contentType = "Navigation Node";
		private const string modelName = "NavigationNode";

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			service = mockery.DynamicMock<IContentService>();

			controller = new ContentController(service);
			PrepareController(controller, "ContentController");
		}

		[Test]
		public void CanListCurrentContentElementsWithNoFilters()
		{
			var data = new List<ContentElement>
			                   	{
			                   		new ContentElement {Id = 1, Name = "Home"},
									new ContentElement {Id = 2, Name = "About Us"}
			                   	};

			With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(service.GetContentModelName(contentType)).Return(modelName);
								Expect.Call(service.GetCurrentElements(modelName)).Return(data);
				           	})
				.Verify(() => controller.List(contentType));

			Assert.AreEqual(1, controller.PropertyBag.Count);
			Assert.AreEqual(2, ((ICollection<ContentElement>)controller.PropertyBag["ListItems"]).Count);
		}

		[Test]
		public void CanListCurrentContentWithFilters()
		{
			
		}

		[Test]
		public void CanPopulateFormForEditing()
		{
			var data = new NavigationNode {Id = 1, Name = "Node"};
			var id = 1;
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetElementItem(contentType, id)).Return(data))
				.Verify(() => controller.Edit(contentType, id));
		}
	}
}