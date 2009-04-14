using System.Collections;
using System.Collections.Generic;
using System.IO;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Helpers;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;

namespace Membrane.ViewComponents
{
	[ViewComponentDetails("FormGenerator", Sections = "startrow,endrow")]
	public class AutomaticFormFieldGeneratorComponent : ViewComponent
	{
		private const string StartRowSection = "startrow";
		private const string EndRowSection = "endrow";

		private const string DefaultStartRow = "<div>";
		private const string DefaultEndRow = "</div>";


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

			//  Has a FieldPrefix been supplied? If so apply it to all of the field ids
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
			formHelper = new FormHelper(EngineContext);
			formHelper.UseJQueryValidation();

			foreach (var field in Fields)
			{
				// Render Start Row
				renderRowSection(StartRowSection, writer, DefaultStartRow);
				switch(field.Type)
				{
					case FieldType.SingleLineTextField:
						renderSingleLineTextField(field, writer);
						break;
					case FieldType.MultiLineTextField:
						renderMultiLineTextField(field, writer);
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
						renderSingleLineTextField(field, writer, "datePicker");
						break;
						
				}

				// Render End Row
				renderRowSection(EndRowSection, writer, DefaultEndRow);
			}

			RenderText(writer.ToString());
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
			renderSingleLineTextField(field, writer, null);
		}

		private void renderSingleLineTextField(FormField field, StringWriter writer, string cssClass)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			if (string.IsNullOrEmpty(cssClass))
				writer.WriteLine(formHelper.TextField(field.Id));
			else
				writer.WriteLine(formHelper.TextField(field.Id, DictHelper.Create(new[] { string.Format("class={0}", cssClass) })));
		}

		private void renderMultiLineTextField(FormField field, StringWriter writer)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			writer.WriteLine(formHelper.TextArea(field.Id, DictHelper.Create(new [] { "cols=20", "rows=50"})));
		}

		private void renderHiddenField(FormField field, StringWriter writer)
		{
			writer.WriteLine(formHelper.HiddenField(field.Id));
		}

		private void renderSelectList(FormField field, StringWriter writer, bool isMultiple)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			if (isMultiple)
				writer.WriteLine(formHelper.Select(field.Id, (IList)PropertyBag[string.Format("support.{0}", field.RelatedTypeName)], DictHelper.Create(new[] { string.Format("value={0}", field.OptionValue), string.Format("text={0}", field.OptionText), "multiple=multiple" })));
			else
				writer.WriteLine(formHelper.Select(field.Id, (IList)PropertyBag[string.Format("support.{0}", field.RelatedTypeName)], DictHelper.Create(new[] { string.Format("value={0}", field.OptionValue), string.Format("text={0}", field.OptionText) })));
		}

		private void renderCheckBox(FormField field, StringWriter writer)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			writer.WriteLine(formHelper.CheckboxField(field.Id));
		}
	}
}