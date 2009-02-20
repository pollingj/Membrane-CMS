using System;
using System.Collections.Generic;
using Membrane.Controllers.MembraneAdmin;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Test.Controllers
{
	/// <summary>
	/// TODO: Switch all Rhino Mocks syntax to the Fluent syntax.
	/// </summary>
    [TestFixture]
    public class NavigationTypeControllerFixture : BaseControllerFixture
    {
        private NavigationTypeController controller;
    	private INavigationService service;

        [SetUp]
        public override void SetUp()
        {
			base.SetUp();
			service = mockery.DynamicMock<INavigationService>();
            controller = new NavigationTypeController(service);
            PrepareController(controller, "NavigationType");
        }

        [Test]
        public void CanListNavigationTypes()
        {
			ICollection<NavigationTypeDTO> response = new List<NavigationTypeDTO>();
			
			using (mockery.Record())
			{
				Expect.Call(service.GetAllNavigationTypes()).Return(response);
			}

			using (mockery.Playback())
        		controller.List();


            Assert.IsNotNull(controller.PropertyBag["NavigationTypes"], "Navigation Types PropertyBag has not been set");
			Assert.AreEqual(@"NavigationType\Action", controller.SelectedViewName, "Expected View was not rendered");
        }

		[Test]
		public void CanRenderCreateViewAndPopulatePropertyBagType()
		{
			controller.Create();

			Assert.AreEqual(typeof(NavigationTypeDTO), controller.PropertyBag["NavigationTypeType"], "Validation PropertyBag not set");
			Assert.AreEqual(@"NavigationType\Action", controller.SelectedViewName, "Expected View was not rendered");
		}

		[Test]
		public void CanSaveNewNavigationTypeAndRedirectToTypeList()
		{
			var navType = new NavigationTypeDTO { Name = "Primary Navigation" };

			using (mockery.Record())
			{
				Expect.Call(service.AddNavigationType(navType)).Return(true);
			}

			using (mockery.Playback())
				controller.Submit(navType);

			Assert.AreEqual(@"NavigationType\Action", controller.SelectedViewName, "The List page was not redirected to");
		}

		[Test]
		public void CanFailValidationWhenCreatingNewNavigationType()
		{
			var navType = new NavigationTypeDTO();

			controller.Validator.IsValid(navType);
			controller.PopulateValidatorErrorSummary(navType, controller.Validator.GetErrorSummary(navType));

			controller.Submit(navType);

			Assert.AreEqual(navType, controller.Flash["NavigationType"], "The Flash memory has not been set");
			Assert.IsNotNull(controller.Flash["ErrorSummary"]);
			Assert.AreEqual(@"NavigationType\Action", controller.SelectedViewName, "Have not been returned to the Creation page");
		}

		[Test]
		public void CanFailAddingToDatabaseWhenCreatingNewNavigationType()
		{
			var navType = new NavigationTypeDTO { Name = "Primary Navigation" };

			using (mockery.Record())
			{
				Expect.Call(service.AddNavigationType(navType)).Return(false);
			}


			using (mockery.Playback())
				controller.Submit(navType);

			Assert.IsTrue(Convert.ToBoolean(controller.Flash["AddFailure"]));
			Assert.AreEqual(navType, controller.Flash["NavigationType"], "The Flash memory has not been set");
			Assert.AreEqual(@"NavigationType\Action", controller.SelectedViewName, "Have not been returned to the Creation page");
		}

		[Test]
		public void CanShowNavigationTypeForEditing()
		{
			var navType = new NavigationTypeDTO {Id = 1, Name = "Primary Navigation"};
			using (mockery.Record())
			{
				//Expect.Call(service.EditNavigationType())
			}
		}
    }

}