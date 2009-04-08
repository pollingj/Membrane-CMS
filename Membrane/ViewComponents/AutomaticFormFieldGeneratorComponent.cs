using System.Collections.Generic;
using System.IO;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Helpers;
using Castle.MonoRail.ViewComponents;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;

namespace Membrane.ViewComponents
{
	public class AutomaticFormFieldGeneratorComponent : ViewComponent
	{
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
				switch(field.Type)
				{
					case FieldType.SingleLineTextField:
						RenderSingleLineTextField(field, writer);
						break;
					case FieldType.MultiLineTextField:
						RenderMultiLineTextField(field, writer);
						break;
					case FieldType.Hidden:
						RenderHiddenField(field, writer);
						break;
				}
			}

			RenderText(writer.ToString());
		}

		private void RenderSingleLineTextField(FormField field, StringWriter writer)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			writer.WriteLine(formHelper.TextField(field.Id));
		}

		private void RenderMultiLineTextField(FormField field, StringWriter writer)
		{
			writer.WriteLine(formHelper.LabelFor(field.Id, field.Label));
			writer.WriteLine(formHelper.TextArea(field.Id));
		}

		private void RenderHiddenField(FormField field, StringWriter writer)
		{
			writer.WriteLine(formHelper.HiddenField(field.Id));
		}
	}
}