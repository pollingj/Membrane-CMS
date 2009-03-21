using System;
using System.Collections;
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
		private PropertyInfo cols;
		private PropertyInfo id;
		private PropertyInfo label;
		private PropertyInfo maxLength;
		private PropertyInfo options;
		private PropertyInfo optionText;
		private PropertyInfo optionValue;
		private PropertyInfo rows;
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
			id = FindElement(source, "FieldId");
			label = FindElement(source, "Label");
			type = FindElement(source, "Type");
			rows = FindElement(source, "Rows");
			cols = FindElement(source, "Cols");
			maxLength = FindElement(source, "MaxLength");
			options = FindElement(source, "Options");
			optionValue = FindElement(source, "OptionValue");
			optionText = FindElement(source, "OptionText");
			uploadLocation = FindElement(source, "UploadLocation");
			uploadWidth = FindElement(source, "UploadWidth");
			uploadHeight = FindElement(source, "UploadHeight");
			source.GetEnumerator().MoveNext();
			var formHelper = (FormHelper) Context.ContextVars["FormHelper"];
			formHelper.UsePrototypeValidation();
			RenderText(formHelper.FormTag("Submit", DictHelper.Create(new [] {"enctype=multipart/form-data", "method=post", "immediate=true", "useLabels=true"})));
			ShowFormItems(source, formHelper);
			RenderText(formHelper.Submit("Submit"));
			RenderText(formHelper.EndFormTag());
			RenderText("<script type=\"text/javascript\">");
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
			RenderText("</script>");
		}

		protected virtual void ShowFormItems(IEnumerable source, FormHelper formHelper)
		{
			var list = new ArrayList();
			foreach (var obj2 in source)
			{
				IList data;
				string str;
				string str2;
				var target = id.GetValue(obj2, null).ToString();
				var label = this.label.GetValue(obj2, null).ToString();
				RenderText("<div>");
				switch (type.GetValue(obj2, null).ToString())
				{
					case "Hidden":
						RenderText(formHelper.HiddenField(target));
						break;

					case "FileUpload":
							var str5 = uploadLocation.GetValue(obj2, null).ToString();
							var num = (int) uploadWidth.GetValue(obj2, null);
							var num2 = (int) uploadHeight.GetValue(obj2, null);
							RenderText(formHelper.LabelFor(target, label));
							RenderText(formHelper.FileField(string.Format("{0}_upload", target)));
							RenderText(formHelper.TextField(target, DictHelper.Create(new [] {"readonly=true", "class=currentFile"})));
							RenderText(formHelper.HiddenField(string.Format("{0}_folder", target), str5));
							RenderText(formHelper.HiddenField(string.Format("{0}_width", target), num));
							RenderText(formHelper.HiddenField(string.Format("{0}_height", target), num2));
							list.Add(string.Format("{0}_upload", target));
							break;
					case "CheckBox":
						RenderText(formHelper.LabelFor(target, label));
						RenderText(formHelper.CheckboxField(target));
						break;

					case "MultipleDropDownList":
					case "SingleDropDownList":
						//array = (Array) options.GetValue(obj2, null);

						data = (IList) PropertyBag[string.Format("support.{0}", target.Remove(0, 5))];
						str = optionValue.GetValue(obj2, null).ToString();
						str2 = optionText.GetValue(obj2, null).ToString();
						RenderText(formHelper.LabelFor(target, label));
						if (type.GetValue(obj2, null).ToString() == "SingleDropDownList")
						{
							target = string.Format("{0}.Id", target);
						}
						if (!(type.GetValue(obj2, null).ToString() == "MultipleDropDownList"))
						{
							break;
						}
						RenderText(formHelper.Select(target, data, DictHelper.Create(new[] { "multiple=true", string.Format("value={0}", str), string.Format("text={0}", str2) })));
						break;

					case "TextArea":
							var num3 = (int) rows.GetValue(obj2, null);
							var num4 = (int) cols.GetValue(obj2, null);
							RenderText(formHelper.LabelFor(target, label));
							RenderText(formHelper.TextArea(target, DictHelper.Create(new [] {string.Format("cols={0}", num4), string.Format("rows={0}", num3)})));
							break;
					case "Date":
						RenderText(formHelper.LabelFor(target, label));
						RenderText(formHelper.TextField(target, DictHelper.Create(new [] {"class=datepicker"})));
						break;

					default:
							var num5 = 0;
							if (maxLength != null)
							{
								num5 = (int) maxLength.GetValue(obj2, null);
							}
							RenderText(formHelper.LabelFor(target, label));
							if (num5 != 0)
							{
								RenderText(formHelper.TextField(target, DictHelper.Create(new [] {string.Format("maxlength={0}", num5)})));
							}
							else
							{
								RenderText(formHelper.TextField(target));
							}
							break;
				}
				//RenderText(formHelper.Select(target, array, DictHelper.Create(new [] {string.Format("value={0}", str), string.Format("text={0}", str2)})));
				RenderText("</div>");
				Context.RenderSection("enditem");
			}
			foreach (string str6 in list)
			{
				RenderText(formHelper.HiddenField("uploadControl", str6));
			}
		}
	}
}