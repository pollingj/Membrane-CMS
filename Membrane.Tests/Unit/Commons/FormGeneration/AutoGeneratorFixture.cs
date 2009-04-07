using System;
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
					case "item.Id":
						AssertFormFieldData("Id", FieldType.Hidden, field);
						break;
					case "item.ProductName":
						AssertFormFieldData("Product Name", FieldType.SingleLineTextField, field);
						break;
					case "item.Price":
						AssertFormFieldData("Price", FieldType.SingleLineTextField, field);
						break;
					case "item.ExpiresOn":
						AssertFormFieldData("Expires On", FieldType.Date, field);
						break;
					case "item.ShowOnHomePage":
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
					case "item.Id":
						AssertFormFieldData("Id", FieldType.Hidden, field);
						break;
					case "item.Title":
						AssertFormFieldData("Title", FieldType.SingleLineTextField, field);
						break;
					case "item.Short_Description_Testing_Underscores":
						AssertFormFieldData("Short Description Testing Underscores", FieldType.MultiLineTextField, field);
						break;
					case "item.NewsImage":
						AssertFormFieldData("News Image", FieldType.FileUpload, field);
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
		public string Short_Description_Testing_Underscores { get; set; }
		[FormFieldType(FieldType.FileUpload)]
		public string NewsImage { get; set; }
		[FormFieldType(FieldType.Ignore)]
		public string IgnoreMeField { get; set; }
	}

	public class Test


}