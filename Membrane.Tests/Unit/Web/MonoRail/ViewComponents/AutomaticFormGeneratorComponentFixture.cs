using System.Collections;
using System.Collections.Generic;
using System.IO;
using Castle.MonoRail.Framework;
using Castle.MonoRail.TestSupport;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.ViewComponents;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.ViewComponents
{
	[TestFixture]
	public class AutomaticFormGeneratorComponentFixture : BaseViewComponentTest
	{
		private AutomaticFormFieldGeneratorComponent component;

		[SetUp]
		public void SetUp()
		{
			component = new AutomaticFormFieldGeneratorComponent();
		}

		[TearDown]
		public void TearDown()
		{
			CleanUp();
		}

		[Test, ExpectedException(typeof(ViewComponentException), "The AutomaticFormItemGenerator requires a view component parameter named 'fields' which should contain 'IList<FormField>' instance")]
		public void ThrowsExceptionIfNoFieldsParameterIsSupplied()
		{
			component.Fields = null;
			component.Initialize();
		}

		[Test]
		public void CanDisplaySimpleFormFields()
		{

			var actions = new List<string>();
			var formFields = new List<FormField>
			                 	{
			                 		new FormField {Id = "Id", Label = "Id", Type = FieldType.Hidden},
			                 		new FormField {Id = "Name", Label = "Name", Type = FieldType.SingleLineTextField},
			                 		new FormField {Id = "Description", Label = "Description", Type = FieldType.MultiLineTextField}
			                 	};
			SectionRender["FormRow"] = ((context, writer) => actions.Add("row"));
		
			component.Fields = formFields;
			PrepareViewComponent(component);
			//controllerContext.PropertyBag["item"]= new {Id = 1, Name = "Test", Description = "wwrwer"};
			component.Render();

			Assert.AreEqual(3, actions.Count);

		}
	}


}