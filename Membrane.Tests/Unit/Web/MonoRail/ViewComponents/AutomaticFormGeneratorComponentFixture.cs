using System.Collections.Generic;
using Castle.MonoRail.TestSupport;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.ViewComponents;
using NUnit.Framework;

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

			PrepareViewComponent(component);
		}

		[TearDown]
		public void TearDown()
		{
			CleanUp();
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

			component.Context.ComponentParameters["source"] = formFields;

			//SectionRender[""]
			
			component.Render();


			//Assert.AreEqual(actions.);

		}
	}


}