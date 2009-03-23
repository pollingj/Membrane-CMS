using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Helpers;
using Castle.MonoRail.ViewComponents;

namespace Membrane.ViewComponents
{
	[ViewComponentDetails("FormGenerator", Sections = "header,footer,item,alternatingitem,enditem")]
	public class FormGeneratorComponent : ViewComponentEx
	{
		// Fields
		private PropertyInfo id;
		private PropertyInfo label;
		private PropertyInfo maxLength;
		private PropertyInfo optionText;
		private PropertyInfo optionValue;
		private PropertyInfo type;
		private PropertyInfo uploadHeight;
		private PropertyInfo uploadLocation;
		private PropertyInfo uploadWidth;

		// Methods
		public override void Render()
		{
			var source = ComponentParams["source"] as IEnumerable;
			if (source == null)
			{
				throw new ViewComponentException("The grid requires a view component parameter named 'source' which should contain 'IEnumerable' instance");
			}

			// Find the specific elements
			id = FindElement(source, "FieldId");
			label = FindElement(source, "Label");
			type = FindElement(source, "Type");
			maxLength = FindElement(source, "MaxLength");
			optionValue = FindElement(source, "OptionValue");
			optionText = FindElement(source, "OptionText");
			uploadLocation = FindElement(source, "UploadLocation");
			uploadWidth = FindElement(source, "UploadWidth");
			uploadHeight = FindElement(source, "UploadHeight");
			source.GetEnumerator().MoveNext();

			// Access the FormHelper 
			var formHelper = (FormHelper) Context.ContextVars["FormHelper"];
			formHelper.UseJQueryValidation();

			// Create surrounding form code
			// TODO: Add Legend to form and possibly h tag
			RenderText(formHelper.FormTag("Submit", DictHelper.Create(new [] {"enctype=multipart/form-data", "method=post", "immediate=true", "useLabels=true"})));
			ShowFormItems(source, formHelper);
			RenderText(formHelper.Submit("Submit"));
			RenderText(formHelper.EndFormTag());

			// TODO: Add jQuery DatePicker code
			/*RenderText("<script type=\"text/javascript\">");
			RenderText("document.observe(\"dom:loaded\", function() {");
			RenderText("var inputs = $A($$('input.datepicker'));");
			RenderText("inputs.each(function(item) {");
			RenderText("new DatePicker({");
			RenderText("relative\t: item,");
			RenderText("language\t: 'en',");
			RenderText("dateFormat  :  [[\"mm\", \"dd\", \"yyyy\"],\"/\"], ");
			RenderText("keepFieldEmpty : true,");
			RenderText("disableFutureDate: false,");
			RenderText("enableShowEffect: false,");
			RenderText("enableCloseEffect: false");
			RenderText("});");
			RenderText("})");
			RenderText("});");
			RenderText("</script>");*/
		}

		protected virtual void ShowFormItems(IEnumerable source, FormHelper formHelper)
		{
			var uploadControlList = new List<string>();
			foreach (var formItem in source)
			{
				var target = id.GetValue(formItem, null).ToString();
				var fieldLabel = this.label.GetValue(formItem, null).ToString();
				RenderText("<div>");

				// What type of form item are we dealing with?
				switch (type.GetValue(formItem, null).ToString())
				{
					case "Hidden":
						RenderText(formHelper.HiddenField(target));
						break;
					case "FileUpload":
						uploadControlList.Add(CreateFileUploadField(formItem, formHelper, target, fieldLabel));
						break;
					case "CheckBox":
						CreateCheckBoxField(formHelper, target, fieldLabel);
						break;
					case "MultipleDropDownList":
					case "SingleDropDownList":
						CreateSelectList(formItem, formHelper, target, fieldLabel);
						break;

					case "TextArea":
						CreateTextAreaField(formHelper, target, fieldLabel);
						break;
					case "Date":
						CreateDatePickerField(formHelper, target, fieldLabel);
						break;

					default:
						CreateTextField(formItem, formHelper, target, fieldLabel);
						break;
				}
				RenderText("</div>");
				Context.RenderSection("enditem");
			}

			// Place all of the upload controls in hidden fields, so we can access them later
			foreach (var controlName in uploadControlList)
			{
				RenderText(formHelper.HiddenField("uploadControl", controlName));
			}
		}

		private void CreateTextField(object formItem, FormHelper formHelper, string target, string itemLabel)
		{
			var num5 = 0;
			if (maxLength != null)
				num5 = (int)maxLength.GetValue(formItem, null);
			RenderText(formHelper.LabelFor(target, itemLabel));
			if (num5 != 0)
			{
				RenderText(formHelper.TextField(target, DictHelper.Create(new [] {string.Format("maxlength={0}", num5)})));
			}
			else
			{
				RenderText(formHelper.TextField(target));
			}
		}

		private void CreateDatePickerField(FormHelper formHelper, string target, string itemLabel)
		{
			RenderText(formHelper.LabelFor(target, itemLabel));
			RenderText(formHelper.TextField(target, DictHelper.Create(new [] {"class=datepicker"})));
		}

		private void CreateTextAreaField(FormHelper formHelper, string target, string itemLabel)
		{
			RenderText(formHelper.LabelFor(target, itemLabel));
			RenderText(formHelper.TextArea(target, DictHelper.Create(new [] { "cols=20", "rows=50"})));
		}

		private void CreateSelectList(object formItem, FormHelper formHelper, string target, string itemLabel)
		{
			var data = (IList) PropertyBag[string.Format("support.{0}", target.Remove(0, 5))];
			var valueField = optionValue.GetValue(formItem, null).ToString();
			var valueText = optionText.GetValue(formItem, null).ToString();
			RenderText(formHelper.LabelFor(target, itemLabel));
			if (type.GetValue(formItem, null).ToString() == "SingleDropDownList")
			{
				target = string.Format("{0}.Id", target);
				RenderText(formHelper.Select(target, data, DictHelper.Create(new[] { string.Format("value={0}", valueField), string.Format("text={0}", valueText) })));
			}
			if (type.GetValue(formItem, null).ToString() == "MultipleDropDownList")
			{
				RenderText(formHelper.Select(target, data, DictHelper.Create(new[] { "multiple=true", string.Format("value={0}", valueField), string.Format("text={0}", valueText) })));
			}
		}

		private void CreateCheckBoxField(FormHelper formHelper, string target, string itemLabel)
		{
			RenderText(formHelper.LabelFor(target, itemLabel));
			RenderText(formHelper.CheckboxField(target));
		}

		private string CreateFileUploadField(object formItem, FormHelper formHelper, string target, string itemLabel)
		{
			var uploadPath = uploadLocation.GetValue(formItem, null).ToString();
			var imageWidth = (int)uploadWidth.GetValue(formItem, null);
			var imageHeight = (int)uploadHeight.GetValue(formItem, null);
			RenderText(formHelper.LabelFor(target, itemLabel));
			RenderText(formHelper.FileField(string.Format("{0}_upload", target)));
			RenderText(formHelper.TextField(target, DictHelper.Create(new [] {"readonly=true", "class=currentFile"})));
			RenderText(formHelper.HiddenField(string.Format("{0}_folder", target), uploadPath));
			RenderText(formHelper.HiddenField(string.Format("{0}_width", target), imageWidth));
			RenderText(formHelper.HiddenField(string.Format("{0}_height", target), imageHeight));

			// We need to return the upload control.
			return string.Format("{0}_upload", target);

		}
	}
}