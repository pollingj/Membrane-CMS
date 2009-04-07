using System;
using System.Collections.Generic;
using Membrane.Commons.CRUD;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Interfaces;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Commons.FormGeneration
{
	[TestFixture]
	public class AutoGeneratorFixture
	{
		[Test]
		public void CanReadSimpleConventionBasedDTOProperties()
		{
			IAutoGenerator<TestSimpleConventionDTO> autoGenerator = new AutoGenerator<TestSimpleConventionDTO>();
			autoGenerator.ReadViewModelProperties();

			Assert.AreEqual(typeof (TestSimpleConventionDTO).GetProperties().Length, autoGenerator.FormFields.Count);

			foreach (var field in autoGenerator.FormFields)
			{
				switch (field.Id)
				{
					case "Id":
						AssertFormFieldData("Id", FieldType.Hidden, field);
						break;
					case "ProductName":
						AssertFormFieldData("Product Name", FieldType.SingleLineTextField, field);
						break;
					case "Price":
						AssertFormFieldData("Price", FieldType.SingleLineTextField, field);
						break;
					case "ExpiresOn":
						AssertFormFieldData("Expires On", FieldType.Date, field);
						break;
					case "ShowOnHomePage":
						AssertFormFieldData("Show On Home Page", FieldType.CheckBox, field);
						break;
				}
			}
		}

		[Test]
		public void CanReadSimpleConfigurationDTOProperties()
		{
			IAutoGenerator<TestSimpleConfigurationDTO> autoGenerator = new AutoGenerator<TestSimpleConfigurationDTO>();
			autoGenerator.ReadViewModelProperties();

			// Remember the 1 ignore field
			Assert.AreEqual(typeof (TestSimpleConventionDTO).GetProperties().Length - 1, autoGenerator.FormFields.Count);

			foreach (var field in autoGenerator.FormFields)
			{
				switch (field.Id)
				{
					case "Id":
						AssertFormFieldData("Id", FieldType.Hidden, field);
						break;
					case "Title":
						AssertFormFieldData("Title", FieldType.SingleLineTextField, field);
						break;
					case "ShortDescription":
						AssertFormFieldData("Short Description", FieldType.MultiLineTextField, field);
						break;
					case "NewsImage":
						AssertFormFieldData("News Image", FieldType.FileUpload, field);
						break;
				}
			}
		}

		[Test]
		public void CanReadBelongsToDTOPropertiesWithConventions()
		{
			IAutoGenerator<ProductDTO> autoGenerator = new AutoGenerator<ProductDTO>();
			autoGenerator.ReadViewModelProperties();

			Assert.AreEqual(typeof(ProductDTO).GetProperties().Length, autoGenerator.FormFields.Count);

			foreach (var field in autoGenerator.FormFields)
			{
				switch (field.Id)
				{
					case "Id":
						AssertFormFieldData("Id", FieldType.Hidden, field);
						break;
					case "Name":
						AssertFormFieldData("Name", FieldType.SingleLineTextField, field);
						break;
					case "Price":
						AssertFormFieldData("Price", FieldType.SingleLineTextField, field);
						break;
					case "Type":
						AssertFormFieldData("Type", FieldType.SingleSelectDropDownList, field);
						Assert.AreEqual("Id", field.OptionValue);
						Assert.AreEqual("Name", field.OptionText);
						break;
				}
			}
		}

		[Test]
		public void CanReadHasAndBelongsToManyDTOPropertiesWithConventions()
		{
			IAutoGenerator<AccessoryDTO> autoGenerator = new AutoGenerator<AccessoryDTO>();
			autoGenerator.ReadViewModelProperties();

			Assert.AreEqual(typeof(AccessoryDTO).GetProperties().Length, autoGenerator.FormFields.Count);

			foreach (var field in autoGenerator.FormFields)
			{
				switch (field.Id)
				{
					case "Id":
						AssertFormFieldData("Id", FieldType.Hidden, field);
						break;
					case "Name":
						AssertFormFieldData("Name", FieldType.SingleLineTextField, field);
						break;
					case "Price":
						AssertFormFieldData("Price", FieldType.SingleLineTextField, field);
						break;
					case "Product":
						AssertFormFieldData("Product", FieldType.MultiSelectDropDownList, field);
						Assert.AreEqual("Id", field.OptionValue);
						Assert.AreEqual("Name", field.OptionText);
						break;
				}
			}
		}

		private void AssertFormFieldData(string expectedLabel, FieldType expectedType, FormField field)
		{
			Assert.AreEqual(expectedLabel, field.Label);
			Assert.AreEqual(expectedType, field.Type);
		}
	}

	#region Test DTOs

	public class TestSimpleConventionDTO : IDTO
	{
		public Guid Id { get; set; }
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public DateTime ExpiresOn { get; set; }
		public bool ShowOnHomePage { get; set; }
	}

	public class TestSimpleConfigurationDTO : IDTO
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		[FormFieldType(FieldType.MultiLineTextField)]
		public string ShortDescription { get; set; }
		[FormFieldType(FieldType.FileUpload)]
		public string NewsImage { get; set; }
		[FormFieldType(FieldType.Ignore)]
		public string IgnoreMeField { get; set; }
	}

	public class ProductTypeDTO : IDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}

	public class ProductDTO : IDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public ProductTypeDTO Type { get; set; }
	}

	public class AccessoryDTO : IDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public IList<ProductDTO> Products { get; set; }
	}

	#endregion


}