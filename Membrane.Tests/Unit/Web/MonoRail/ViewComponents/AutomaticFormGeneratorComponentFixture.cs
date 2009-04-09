using System.Collections;
using System.Collections.Generic;
using Castle.MonoRail.Framework;
using Castle.MonoRail.TestSupport;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.ViewComponents;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.ViewComponents
{
	/// <summary>
	/// Test fixture for making sure the AutomaticFormGeneratorComponent is rendering form items correctly
	/// </summary>
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

		/// <summary>
		/// Tests if some simple
		/// </summary>
		[Test]
		public void CanDisplaySimpleFormFields()
		{
			RunAndCheckViewComponentOutput("<div>\r\n<input type=\"hidden\" id=\"item_Id\" name=\"item.Id\" value=\"\" />\r\n</div>\r\n<div>\r\n<label for=\"item_Name\">Name</label>\r\n<input type=\"text\" id=\"item_Name\" name=\"item.Name\" value=\"\" />\r\n</div>\r\n<div>\r\n<label for=\"item_Description\">Description</label>\r\n<textarea id=\"item_Description\" name=\"item.Description\" cols=\"20\" rows=\"50\" ></textarea>\r\n</div>\r\n");
		}


		/// <summary>
		/// Tests if a Single Select Drop Down List can be displayed
		/// Needs to check if the related data has been loaded.
		/// </summary>
		[Test]
		public void CanDisplaySingleSelectDropDownList()
		{
	
			formFields.Add(new FormField { Id = "ProductType", Label = "Product Type", OptionValue = "Id", OptionText = "Name", Type = FieldType.SingleSelectDropDownList});

			// Need to make sure a call to load the related data is called.

			RunAndCheckViewComponentOutput("<div>\r\n<input type=\"hidden\" id=\"item_Id\" name=\"item.Id\" value=\"\" />\r\n</div>\r\n<div>\r\n<label for=\"item_Name\">Name</label>\r\n<input type=\"text\" id=\"item_Name\" name=\"item.Name\" value=\"\" />\r\n</div>\r\n<div>\r\n<label for=\"item_Description\">Description</label>\r\n<textarea id=\"item_Description\" name=\"item.Description\" cols=\"20\" rows=\"50\" ></textarea>\r\n</div>\r\n<div>\r\n<label for=\"item_ProductType\">Product Type</label>\r\n<select id=\"item.ProductType\" name=\"item_ProductType\">\r\n</select>\r\n</div>\r\n");

		}

		private void RunAndCheckViewComponentOutput(string expectedHtml)
		{
			var actions = new List<string>();

			SectionRender["startrow"] = ((context, writer) =>
			{
				actions.Add("startrow");
				writer.WriteLine("<div>");
			});
			SectionRender["endrow"] = ((context, writer) =>
			{
				actions.Add("endrow");
				writer.WriteLine("</div>");
			});

			component.FieldPrefix = "item";
			component.Fields = formFields;

			// Must provide a ControllerContext.  
			PrepareViewComponent(component);

			Context.CurrentControllerContext = mockery.Stub<IControllerContext>();
			Context.CurrentControllerContext.PropertyBag = new Hashtable();

			component.Render();

			Assert.AreEqual(formFields.Count * 2, actions.Count);
			Assert.AreEqual(expectedHtml, Output);
		}
	}


}