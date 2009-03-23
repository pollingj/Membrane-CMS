using System;
using System.Collections;

using System.Reflection;
using System.Text.RegularExpressions;

namespace Membrane.Commons.Scaffolding
{
	public class FormItem
	{


		public int Cols { get; set; }
		public string FieldId { get; set; }
		public string Label { get; set; }
		public int MaxLength { get; set; }
		public Array Options { get; set; }
		public string OptionText { get; set; }
		public string OptionValue { get; set; }
		public int Rows { get; set; }
		public FieldType Type { get; set; }
		public int UploadHeight { get; set; }
		public string UploadLocation { get; set; }
		public int UploadWidth { get; set; }

		// Methods
		public FormItem()
		{
		}

		public FormItem(string fieldId, string label, FieldType type)
		{
			FieldId = fieldId;
			Label = label;
			Type = type;
		}

		private static void CreateDefaultTypes(PropertyInfo prop, FormItem formItem)
		{
			switch (prop.PropertyType.Name)
			{
				case "Guid":
					formItem.Type = FieldType.Hidden;
					break;
				case "String":
				case "Decimal":
				case "Double":
				case "Float":
				case "Int16":
				case "Int32":
				case "Int64":
				case "NullableInt16":
				case "NullableInt32":
				case "NullableInt64":
				case "NullableFloat":
				case "NullableDecimal":
					formItem.Type = FieldType.TextBox;
					break;

				case "DateTime":
				case "NullableDateTime":
					formItem.Type = FieldType.Date;
					break;

				case "Boolean":
				case "NullableBoolean":
					formItem.Type = FieldType.CheckBox;
					break;

				default:
					formItem.Type = FieldType.Ignore;
					break;
			}
		}

		public static FormItem[] GetFields<T>()
		{
			return GetFieldsWithVals(default(T));
		}

		public static FormItem[] GetFieldsWithVals<T>(T data, params object[] supportData)
		{
			var list = new ArrayList();
			var modelClass = typeof (T);

			if (modelClass == null)
			{
				throw new Exception(string.Format("No such model {0} exists", typeof(T)));
			}

			var properties = modelClass.GetProperties();
			foreach (var info in properties)
			{
				var hasFieldTypeAttribute = false;
				var customAttributes = info.GetCustomAttributes(true);
				var formItem = new FormItem { 
					FieldId = string.Format("item.{0}", info.Name),
					Label = CreateLabel(info.Name)
				};

				// Are there any custom attributes to configured for this property?
				if (customAttributes.Length > 0)
				{
					foreach (var attribute in customAttributes)
					{
						// Are we handling a FieldTypeAttr?
						if (attribute.GetType() == typeof(FieldTypeAttr))
						{
							hasFieldTypeAttribute = true;
							var attr = (FieldTypeAttr)attribute;
							formItem.Type = attr.Type;
							if (attr.Label != null)
							{
								formItem.Label = attr.Label;
							}
							if (attr.MaxLength != 0)
							{
								formItem.MaxLength = attr.MaxLength;
							}

							switch (attr.Type)
							{
								case FieldType.SingleDropDownList:
								case FieldType.MultipleDropDownList:
									formItem.OptionValue = attr.OptionValue;
									formItem.OptionText = attr.OptionText;
									break;
								case FieldType.FileUpload:
									formItem.UploadLocation = attr.UploadFolder;
									formItem.UploadWidth = attr.ImageWidth;
									formItem.UploadHeight = attr.ImageHeight;
									break;
							}
						}
						else if (!hasFieldTypeAttribute)
						{
							CreateDefaultTypes(info, formItem);
						}
					}
				}
				else
				{
					CreateDefaultTypes(info, formItem);
				}

				if (formItem.Type != FieldType.Ignore)
				{
					list.Add(formItem);
				}
			}
			return (FormItem[])list.ToArray(typeof(FormItem));
		}


		private static string CreateLabel(string typeName)
		{
			var r = new Regex("([A-Z]+[a-z]+)");
			return r.Replace(typeName, m => (m.Value.Length > 3 ? m.Value : m.Value.ToLower()) + " ");

		}

	}
}