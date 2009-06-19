using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Helpers;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Exceptions;
using Membrane.Commons.Plugin.DTOs;

namespace Membrane.ViewComponents
{
	[ViewComponentDetails("FormGenerator", Sections = "startrow,endrow")]
	public class AutomaticFormFieldGeneratorComponent : ViewComponent
	{
		private const string STARTROWSECTION = "startrow";
		private const string ENDROWSECTION = "endrow";

		private const string DEFAULTSTARTROW = "<div>";
		private const string DEFAULTENDROW = "</div>";

		private const string TEXTEDITORCLASS = "texteditor";


		[ViewComponentParam(Required = true)]
		public IList<FormField> Fields { get; set; }

		[ViewComponentParam]
		public string FieldPrefix { get; set; }

		private FormHelper formHelper;

		public override void Initialize()
		{
			if (Fields == null)
			{
				throw new ViewComponentException("The AutomaticFormItemGenerator requires a view component parameter named 'fields' which should contain 'IList<FormField>' instance");
			}

			//  Has a FieldPrefix been supplied? If so apply it to all of the field Ids
			if (FieldPrefix != null)
			{
				foreach (var field in Fields)
				{
					field.Id = string.Format("{0}.{1}", FieldPrefix, field.Id);
				}
			}
		}

		public override void Render()
		{
			var writer = new StringWriter();
			formHelper = (FormHelper) EngineContext.CurrentControllerContext.Helpers["Form"];
			formHelper.UseJQueryValidation();
			foreach (var field in Fields)
			{
				// Render Start Row
				renderRowSection(STARTROWSECTION, writer, DEFAULTSTARTROW);
				switch(field.Type)
				{
					case FieldType.SingleLineTextField:
						renderSingleLineTextField(field, writer);
						break;
					case FieldType.MultiLineTextField:
						renderMultiLineTextField(field, writer);
						break;
					case FieldType.TextEditor:
						renderMultiLineTextField(field, writer, TEXTEDITORCLASS);
						break;
					case FieldType.Hidden:
						renderHiddenField(field, writer);
						break;
					case FieldType.SingleSelectDropDownList:
						renderSelectList(field, writer, false);
						break;
					case FieldType.MultiSelectDropDownList:
						renderSelectList(field, writer, true);
						break;
					case FieldType.CheckBox:
						renderCheckBox(field, writer);
						break;
					case FieldType.Date:
						renderDateField(field, writer);
						break;
					default:
						throw new NoRenderMethodForFormFieldType();
						
				}

				// Render End Row
				renderRowSection(ENDROWSECTION, writer, DEFAULTENDROW);
			}

			RenderText(writer.ToString());
		}

		private void renderDateField(FormField field, StringWriter writer)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			writer.WriteLine(formHelper.TextField(field.Id, DictHelper.Create(new[] { "class=datePicker", "textformat=dd/MM/yyyy" })));
		}

		private void renderRowSection(string section, StringWriter writer, string defaultHTML)
		{
			if (Context.HasSection(section))
				Context.RenderSection(section, writer);
			else
				writer.WriteLine(defaultHTML);
			
		}

		private void renderSingleLineTextField(FormField field, StringWriter writer)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			writer.WriteLine(formHelper.TextField(field.Id));
		}

		private void renderMultiLineTextField(FormField field, StringWriter writer)
		{
			renderMultiLineTextField(field, writer, null);
		}

		private void renderMultiLineTextField(FormField field, StringWriter writer, string cssClass)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			if (string.IsNullOrEmpty(cssClass))
				writer.WriteLine(formHelper.TextArea(field.Id, DictHelper.Create(new[] { "cols=20", "rows=50" })));
			else
				writer.WriteLine(formHelper.TextArea(field.Id, DictHelper.Create(new[] { "cols=20", "rows=50", string.Format("class={0}", cssClass) })));
		}

		private void renderHiddenField(FormField field, StringWriter writer)
		{
			if (field.Label == "Culture")
				writer.WriteLine(formHelper.HiddenField(string.Format("{0}.Id", field.Id), ((CultureDTO)Session["Culture"]).Id));
			else
				writer.WriteLine(formHelper.HiddenField(field.Id));
		}

		private void renderSelectList(FormField field, StringWriter writer, bool isMultiple)
		{

			if (isMultiple)
			{
				writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
				writer.WriteLine(formHelper.Select(field.Id, (IList)PropertyBag[string.Format("support.{0}", field.RelatedTypeName)], DictHelper.Create(new[] { string.Format("value={0}", field.OptionValue), string.Format("text={0}", field.OptionText), "multiple=multiple" })));
			}
			else
			{
				writer.WriteLine(formHelper.LabelFor(string.Format("{0}.Id", field.Id), field.Label));
				writer.WriteLine(formHelper.Select(string.Format("{0}.Id", field.Id), (IList) PropertyBag[string.Format("support.{0}", field.RelatedTypeName)], DictHelper.Create(new[] {string.Format("value={0}", field.OptionValue), string.Format("text={0}", field.OptionText), "firstoption=Please select", "firstoptionvalue="})));
			}
		}

		private void renderCheckBox(FormField field, StringWriter writer)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			writer.WriteLine(formHelper.CheckboxField(field.Id));
		}
	}
}