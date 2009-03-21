using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using Castle.Windsor;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.NHibernate;
using Membrane.Commons.Services;

namespace Membrane.Commons.Scaffolding
{
	public class FormItem
	{
		public static string assembly;

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
			//assembly = Assembly.Load()
			ArrayList list = new ArrayList();
			Type modelClass = typeof (T);
			if (modelClass == null)
			{
				throw new Exception(string.Format("No such model {0} exists in {1}", typeof(T), assembly));
			}
			PropertyInfo[] properties = modelClass.GetProperties();
			foreach (PropertyInfo info in properties)
			{
				bool flag = false;
				object[] customAttributes = info.GetCustomAttributes(true);
				FormItem formItem = new FormItem();
				formItem.FieldId = string.Format("item.{0}", info.Name);
				formItem.Label = info.Name;
				if (customAttributes.Length > 0)
				{
					foreach (object obj2 in customAttributes)
					{
						if (obj2.GetType() == typeof(FieldTypeAttr))
						{
							flag = true;
							FieldTypeAttr attr = (FieldTypeAttr)obj2;
							formItem.Type = attr.Type;
							if (attr.Label != null)
							{
								formItem.Label = attr.Label;
							}
							if (attr.MaxLength != 0)
							{
								formItem.MaxLength = attr.MaxLength;
							}
							if (((data != null) && (info.GetValue(data, null) != null)) && ((attr.Type == FieldType.MultipleDropDownList) || (attr.Type == FieldType.SingleDropDownList)))
							{
								ArrayList list2 = new ArrayList();
								foreach (object obj3 in (IEnumerable)info.GetValue(data, null))
								{
									list2.Add(obj3.GetType().GetProperty(attr.OptionValue).GetValue(obj3, null));
								}
							}
							if (attr.Type == FieldType.TextArea)
							{
								formItem.Rows = attr.Rows;
								formItem.Cols = attr.Cols;
							}
							if ((attr.Type == FieldType.MultipleDropDownList) || (attr.Type == FieldType.SingleDropDownList))
							{
								/*WindsorContainer container = new WindsorContainer();
								var rep = container.Resolve("repository");
								Type[] typeArguments = new Type[] { attr.Options };
								object target = Activator.CreateInstance(typeof(BaseCrudService<>).MakeGenericType(typeArguments), AppDomain.CurrentDomain.);
								object[] objArray2 = (object[])target.GetType().InvokeMember("GetAllData", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, null, target, null);
								//formItem.Options = attr.Options;
								//formItem.Options = (Array)supportData[0];
								formItem.Options = objArray2;*/
								formItem.OptionValue = attr.OptionValue;
								formItem.OptionText = attr.OptionText;
							}
							else if (attr.Type == FieldType.FileUpload)
							{
								formItem.UploadLocation = attr.UploadFolder;
								formItem.UploadWidth = attr.ImageWidth;
								formItem.UploadHeight = attr.ImageHeight;
							}
						}
						else if (!flag)
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

		public static Type GetModelClass(string typename)
		{
			return GetModelClass(assembly, typename);
		}

		private static Type GetModelClass<T>(string assembly)
		{
			Type[] types = Assembly.LoadFrom(assembly).GetTypes();
			foreach (Type type in types)
			{
				if (type == typeof(T))
				{
					return type;
				}
			}
			return null;
		}

		private static Type GetModelClass(string assembly, string typename)
		{
			Type[] types = Assembly.LoadFrom(assembly).GetTypes();
			foreach (Type type in types)
			{
				if (type.Name == typename)
				{
					return type;
				}
			}
			return null;
		}


	}
}