using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Exceptions;
using Membrane.Commons.FormGeneration.Services.Interfaces;

namespace Membrane.Commons.FormGeneration.Services
{
	public class PropertyReaderService<T> : IPropertyReaderService<T>
	{
		public IList<FormField> FormFields { get; set; }

		/// <summary>
		/// Reads all of the properties found in a object.
		/// If a <see cref="FormFieldTypeAttribute"/> is found then the parameters of the attribute are used to set up the <see cref="FormField"/>.
		/// If no attribute is found then a number of conventions are adhered to create the <see cref="FormField"/> object.
		/// </summary>
		public void ReadViewModelProperties()
		{
			FormFields = new List<FormField>();
			foreach (var propertyInfo in typeof(T).GetProperties())
			{
				var formField = new FormField { Id = propertyInfo.Name, Label = createLabel(propertyInfo.Name)};

				var formFieldAttributes = propertyInfo.GetCustomAttributes(typeof (FormFieldTypeAttribute), true);

				if (formFieldAttributes.Length > 0)
				{
					formField.Type = ((FormFieldTypeAttribute) formFieldAttributes[0]).Type;
					if (formField.Type == FieldType.SingleSelectDropDownList || formField.Type == FieldType.MultiSelectDropDownList)
						getConfigurationBasedOptionsValueAndText((FormFieldTypeAttribute) formFieldAttributes[0], formField);
				}
				else
					getConventionBasedFields(formField, propertyInfo.PropertyType);
					
					
				if (formField.Type != FieldType.Ignore)
					FormFields.Add(formField);
			}
		}

		private void getConventionBasedFields(FormField field, Type propertyType)
		{
			// Are we handling a BelongsTo relationship?
			// Assume that the first and second properties in the related property are the option value and text fields respectively
			if (propertyType.GetInterface("IDto") != null)
			{
				field.Type = FieldType.SingleSelectDropDownList;
				getConventionBasedOptionsValueAndText(propertyType, field);
				field.RelatedTypeName = propertyType.Name;
			}
			else if (propertyType.Name.Contains("List") && propertyType.IsGenericType)
			{
				// Assume we are handling a has and belong to many relationship
				field.Type = FieldType.MultiSelectDropDownList;
				getConventionBasedOptionsValueAndText(propertyType.GetGenericArguments()[0], field);
			}
			// The OrderPosition property needs to be set 
			else if (field.Id == "OrderPosition")
			{
				field.Type = FieldType.Hidden;
			}
			else
			{
				switch (propertyType.Name)
				{
					case "Guid":
						field.Type = FieldType.Hidden;
						break;
					case "String":
					case "Char":
					case "Decimal":
					case "Double":
					case "Float":
					case "Int16":
					case "Int32":
					case "Int64":
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

		private void getConventionBasedOptionsValueAndText(Type propertyType, FormField field)
		{
			var relatedDTOProperties = propertyType.GetProperties();

			if (relatedDTOProperties.Length < 2)
				throw new RelatedDTONeedsAtLeastTwoPropertiesException();

			field.OptionValue = relatedDTOProperties[0].Name;
			field.OptionText = relatedDTOProperties[1].Name;
		}

		private void getConfigurationBasedOptionsValueAndText(FormFieldTypeAttribute attribute, FormField field)
		{
			field.OptionValue = attribute.OptionValue;
			field.OptionText = attribute.OptionText;
		}


		/// <summary>
		/// Method to take a PascalCase property name and turn it into a space delimited string
		/// </summary>
		/// <param name="propertyName">The Property Name</param>
		/// <returns>A space delimited string</returns>
		private string createLabel(string propertyName)
		{
			var r = new Regex("([A-Z]+[a-z]+)");
			var label = r.Replace(propertyName, m => m.Value + " ");

			// Remove the last space
			return label.Remove(label.Length-1);
		}
	}
}