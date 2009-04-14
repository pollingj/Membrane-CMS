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
		private IList actions;

		private const string Basicfieldhtml = "<div>\r\n<input type=\"hidden\" id=\"item_Id\" name=\"item.Id\" value=\"\" />\r\n</div>\r\n<div>\r\n<label for=\"item_Name\">Name</label>\r\n<input type=\"text\" id=\"item_Name\" name=\"item.Name\" value=\"\" />\r\n</div>\r\n<div>\r\n<label for=\"item_Link\">Link</label>\r\n<textarea id=\"item_Link\" name=\"item.Link\" cols=\"20\" rows=\"50\" ></textarea>\r\n</div>\r\n";

		[SetUp]
		public void SetUp()
		{
			mockery = new MockRepository();
			component = new AutomaticFormFieldGeneratorComponent();

			formFields = new List<FormField>
			                 	{
			                 		new FormField {Id = "Id", Label = "Id", Type = FieldType.Hidden},
			                 		new FormField {Id = "Name", Label = "Name", Type = FieldType.SingleLineTextField},
			                 		new FormField {Id = "Link", Label = "Link", Type = FieldType.MultiLineTextField}
			                 	};

			BuildViewComponentContext("FormGenerator");
			BuildEngineContext("Area", "Controller", "Action");
			Context.CurrentControllerContext = mockery.Stub<IControllerContext>();
			Context.CurrentControllerContext.PropertyBag = new Hashtable();
			
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
			SetupViewComponent();

			RunAndCheckViewComponentOutput(Basicfieldhtml);
		}


		/// <summary>
		/// Tests if a Single Select Drop Down List can be displayed
		/// Needs to check if the related data has been loaded.
		/// </summary>
		[Test]
		public void CanDisplaySingleSelectDropDownList()
		{

			formFields.Add(new FormField { Id = "NavigationType", Label = "Navigation Type", OptionValue = "Id", OptionText = "Type", Type = FieldType.SingleSelectDropDownList, RelatedTypeName = "NavigationTypeDTO"});

			SetupViewComponent();
			// Need to make sure a call to load the related data is called.
			component.Context.ContextVars["support.NavigationTypeDTO"] = new ArrayList
			                                                                         	{
																							new { Id = 1, Type = "Primary Navigation"},
																							new { Id = 2, Type = "Secondary Navigation"},
																							new { Id = 3, Type = "Tertiary Navigation"}
			                                                                         	};

			RunAndCheckViewComponentOutput(string.Format("{0}<div>\r\n<label for=\"item_NavigationType_Id\">Navigation Type</label>\r\n<select id=\"item_NavigationType_Id\" name=\"item.NavigationType.Id\" >\r\n<option value=\"\">Please select</option>\r\n<option value=\"1\">Primary Navigation</option>\r\n<option value=\"2\">Secondary Navigation</option>\r\n<option value=\"3\">Tertiary Navigation</option>\r\n</select>\r\n</div>\r\n", Basicfieldhtml));
		}

		[Test]
		public void CanDisplayMultiSelectDropDownList()
		{
			formFields.Add(new FormField { Id = "NavigationType", Label = "Navigation Type", OptionValue = "Id", OptionText = "Type", Type = FieldType.MultiSelectDropDownList, RelatedTypeName = "NavigationTypeDTO" });

			SetupViewComponent();

			// Need to make sure a call to load the related data is called.
			component.Context.ContextVars["support.NavigationTypeDTO"] = new ArrayList
			                                                                         	{
																							new { Id = 1, Type = "Primary Navigation"},
																							new { Id = 2, Type = "Secondary Navigation"},
																							new { Id = 3, Type = "Tertiary Navigation"}
			                                                                         	};

			RunAndCheckViewComponentOutput(string.Format("{0}<div>\r\n<label for=\"item_NavigationType\">Navigation Type</label>\r\n<select id=\"item_NavigationType\" name=\"item.NavigationType\" multiple=\"multiple\" >\r\n<option value=\"1\">Primary Navigation</option>\r\n<option value=\"2\">Secondary Navigation</option>\r\n<option value=\"3\">Tertiary Navigation</option>\r\n</select>\r\n</div>\r\n", Basicfieldhtml));

		}

		[Test]
		public void CanDisplayCheckBoxForBooleanValues()
		{
			formFields.Add(new FormField { Id = "IsLive", Label = "Is Live", Type = FieldType.CheckBox });

			SetupViewComponent();

			RunAndCheckViewComponentOutput(string.Format("{0}<div>\r\n<label for=\"item_IsLive\">Is Live</label>\r\n<input type=\"checkbox\" id=\"item_IsLive\" name=\"item.IsLive\" value=\"true\" /><input type=\"hidden\" id=\"item_IsLiveH\" name=\"item.IsLive\" value=\"false\" />\r\n</div>\r\n", Basicfieldhtml));
		}

		[Test]
		public void CanDisplayDateTimeValues()
		{
			formFields.Add(new FormField {Id = "ExpiresOn", Label = "Expires On", Type = FieldType.Date});

			SetupViewComponent();

			RunAndCheckViewComponentOutput(string.Format("{0}<div>\r\n<label for=\"item_ExpiresOn\">Expires On</label>\r\n<input type=\"text\" id=\"item_ExpiresOn\" name=\"item.ExpiresOn\" value=\"\" class=\"datePicker\" />\r\n</div>\r\n", Basicfieldhtml));
		}

		private void SetupViewComponent()
		{
			actions = new List<string>();

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


			PrepareViewComponent(component);	
		}

		private void RunAndCheckViewComponentOutput(string expectedHtml)
		{

			component.Render();

			Assert.AreEqual(formFields.Count * 2, actions.Count);
			Assert.AreEqual(expectedHtml, Output);
		}
	}


}