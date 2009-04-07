using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Exceptions;
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

				if (formFieldAttributes.Length > 0)
					formField.Type = ((FormFieldTypeAttribute) formFieldAttributes[0]).Type;
				else
					GetConventionBasedFields(formField, propertyInfo.PropertyType);
					
					
				if (formField.Type != FieldType.Ignore)
					FormFields.Add(formField);
			}
		}

		private void GetConventionBasedFields(FormField field, Type propertyType)
		{
			// Are we handling a BelongsTo relationship?
			// Assume that the first and second properties in the related property are the option value and text fields respectively
			if (propertyType.GetInterface("IDTO") != null)
			{
				field.Type = FieldType.SingleSelectDropDownList;
				GetConventionBasedOptionsValueAndText(propertyType, field);
			}
			else if (propertyType.Name.Contains("List") && propertyType.IsGenericType)
			{
				// Assume we are handling a has and belong to many relationship
				field.Type = FieldType.MultiSelectDropDownList;
				GetConventionBasedOptionsValueAndText(propertyType.GetGenericArguments()[0], field);
			}
			else
			{
				switch (propertyType.Name)
				{
					case "Guid":
						field.Type = FieldType.Hidden;
						break;
					case "String":
					case "Decimal":
					case "Double":
						field.Type = FieldType.SingleLineTextField;
						break;

					case "DateTime":
						field.Type = FieldType.Date;
						break;

					case "Boolean":
						field.Type = FieldType.CheckBox;
						break;
					default:
						field.Type = FieldType.Ignore;
						break;
				}
			}

		}

		private void GetConventionBasedOptionsValueAndText(Type propertyType, FormField field)
		{
			var relatedDTOProperties = propertyType.GetProperties();

			if (relatedDTOProperties.Length < 2)
				throw new RelatedDTONeedsAtLeastTwoPropertiesException();

			field.OptionValue = relatedDTOProperties[0].Name;
			field.OptionText = relatedDTOProperties[1].Name;
		}


		/// <summary>
		/// Method to take a PascalCase property name and turn it into a space delimited string
		/// </summary>
		/// <param name="propertyName">The Property Name</param>
		/// <returns>A space delimited string</returns>
		private static string CreateLabel(string propertyName)
		{
			var r = new Regex("([A-Z]+[a-z]+)");
			var label = r.Replace(propertyName, m => m.Value + " ");

			// Remove the last space
			return label.Remove(label.Length-1);
		}
	}


}