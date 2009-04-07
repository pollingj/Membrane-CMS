using System.Collections.Generic;
using System.Text.RegularExpressions;
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
				var formField = new FormField { Label = CreateLabel(propertyInfo.Name) };

				switch (propertyInfo.PropertyType.Name)
				{
					case "Guid":
						formField.Type = FieldType.Hidden;
						break;
					case "String":
					case "Decimal":
					case "Double":
						formField.Type = FieldType.SingleLineTextField;
						break;

					case "DateTime":
						formField.Type = FieldType.Date;
						break;

					case "Boolean":
						formField.Type = FieldType.CheckBox;
						break;
				}

				FormFields.Add(formField);
			}
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