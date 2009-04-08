using System.Collections.Generic;
using System.IO;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Helpers;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;

namespace Membrane.ViewComponents
{
	public class AutomaticFormFieldGeneratorComponent : ViewComponent
	{
		[ViewComponentParam(Required = true)]
		public IList<FormField> Fields { get; set; }

		private FormHelper formHelper;

		public override void Initialize()
		{
			if (Fields == null)
			{
				throw new ViewComponentException("The AutomaticFormItemGenerator requires a view component parameter named 'fields' which should contain 'IList<FormField>' instance");
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
						RenderSingleTextField(field, writer);
						break;
				}
			}
		}

		private void RenderSingleTextField(FormField field, StringWriter writer)
		{
			var fieldId = string.Format("item.{0}", field.Id);
			writer.WriteLine(formHelper.LabelFor(fieldId, field.Label));
			writer.WriteLine(formHelper.TextField(fieldId, DictHelper.Create(new string[] {"maxlength = 100"})));
		}
	}
}