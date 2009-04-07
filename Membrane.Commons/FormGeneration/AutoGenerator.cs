using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Interfaces;

namespace Membrane.Commons.FormGeneration
{
	public class AutoGenerator<T> : IAutoGenerator<T>
	{
		public IList<FormField> FormFields { get; set; }

		public AutoGenerator()
		{
			FormFields = new List<FormField>();
		}

		public void ReadViewModelProperties()
		{
			foreach (var propertyInfo in typeof(T).GetProperties())
			{
				var formField = new FormField { Id = propertyInfo.Name, Label = CreateLabel(propertyInfo.Name)};

				var formFieldAttributes = propertyInfo.GetCustomAttributes(typeof (FormFieldTypeAttribute), true);
				formField.Type = formFieldAttributes.Length > 0 ? ((FormFieldTypeAttribute) formFieldAttributes[0]).Type : GetConventionFieldType(propertyInfo.PropertyType.Name);

				if (formField.Type != FieldType.Ignore)
					FormFields.Add(formField);
			}
		}

		private FieldType GetConventionFieldType(string name)
		{
			var type = new FieldType();
			switch (name)
			{
				case "Guid":
					type = FieldType.Hidden;
					break;
				case "String":
				case "Decimal":
				case "Double":
					type = FieldType.SingleLineTextField;
					break;

				case "DateTime":
					type = FieldType.Date;
					break;

				case "Boolean":
					type = FieldType.CheckBox;
					break;
			}

			return type;
		}


		/// <summary>
		/// Method to take a PascalCase property name and turn it into a space delimited string
		/// </summary>
		/// <param name="propertyName">The Property Name</param>
		/// <returns>A space delimited string</returns>
		private static string CreateLabel(string propertyName)
		{
			var r = new Regex("([A-Z]+[a-z]+)");
			return r.Replace(propertyName, m => (m.Value.Length > 3 ? m.Value : m.Value.ToLower()) + " ");
		}
	}
}