using System.Collections;
using System.Collections.Generic;
using System.IO;
using Castle.MonoRail.Framework;
using Castle.MonoRail.TestSupport;
using Membrane.Commons.CRUD.Services;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Controllers.Administrator;
using Membrane.Core.DTOs;
using Membrane.Entities;
using Membrane.ViewComponents;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.ViewComponents
{
	[TestFixture]
	public class AutomaticFormGeneratorComponentFixture : BaseViewComponentTest
	{
		private AutomaticFormFieldGeneratorComponent component;
		private MockRepository mockery;

		private List<FormField> formFields;

		[SetUp]
		public void SetUp()
		{
			mockery = new MockRepository();
			component = new AutomaticFormFieldGeneratorComponent();

			formFields = new List<FormField>
			                 	{
			                 		new FormField {Id = "Id", Label = "Id", Type = FieldType.Hidden},
			                 		new FormField {Id = "Name", Label = "Name", Type = FieldType.SingleLineTextField},
			                 		new FormField {Id = "Description", Label = "Description", Type = FieldType.MultiLineTextField}
			                 	};

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
		public void CanApplyFormFieldPrefix()
		{
			component.FieldPrefix = "data";
			component.Fields = formFields;
			component.Initialize();

			foreach (var field in component.Fields)
			{
				Assert.IsTrue(field.Id.Contains(component.FieldPrefix));
			}
		}

		[Test]
		public void CanDisplaySectionsCorrectly()
		{

			var actions = new List<string>();

			SectionRender["startrow"] = ((context, writer) => actions.Add("startrow"));
			SectionRender["endrow"] = ((context, writer) => actions.Add("endrow"));


			component.FieldPrefix = "item";
			component.Fields = formFields;

			PrepareViewComponent(component);

			// Must provide a ControllerContext.  
			Context.CurrentControllerContext = mockery.Stub<IControllerContext>();
			Context.CurrentControllerContext.PropertyBag = new Hashtable();

			component.Render();

			Assert.AreEqual(formFields.Count * 2, actions.Count);

		}
	}


}