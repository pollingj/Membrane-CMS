using System;
using System.Collections.Generic;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Attributes;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Services;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Plugin.DTOs;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Commons.FormGeneration.Services
{
	[TestFixture]
	public class PropertyReaderServiceFixture
	{
		[Test]
		public void CanReadSimpleConventionBasedDTOProperties()
		{
			IPropertyReaderService<TestSimpleConventionDto> propertyReaderService = new PropertyReaderService<TestSimpleConventionDto>();
			propertyReaderService.ReadViewModelProperties();

			Assert.AreEqual(typeof(TestSimpleConventionDto).GetProperties().Length, propertyReaderService.FormFields.Count);

			foreach (var field in propertyReaderService.FormFields)
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
			IPropertyReaderService<TestSimpleConfigurationDTO> propertyReaderService = new PropertyReaderService<TestSimpleConfigurationDTO>();
			propertyReaderService.ReadViewModelProperties();

			// Remember the 1 ignore field
			Assert.AreEqual(typeof(TestSimpleConfigurationDTO).GetProperties().Length - 1, propertyReaderService.FormFields.Count);

			foreach (var field in propertyReaderService.FormFields)
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
						AssertFormFieldData("News Image", FieldType.Asset, field);
						break;
				}
			}
		}

		[Test]
		public void CanReadBelongsToDTOPropertiesWithConventions()
		{
			IPropertyReaderService<ProductDTO> propertyReaderService = new PropertyReaderService<ProductDTO>();
			propertyReaderService.ReadViewModelProperties();

			Assert.AreEqual(typeof(ProductDTO).GetProperties().Length, propertyReaderService.FormFields.Count);

			foreach (var field in propertyReaderService.FormFields)
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
					case "StockCount":
						AssertFormFieldData("Stock Count", FieldType.SingleLineTextField, field);
						break;
					case "Type":
						AssertFormFieldData("Type", FieldType.SingleSelectDropDownList, field);
						Assert.AreEqual("Id", field.OptionValue);
						Assert.AreEqual("Name", field.OptionText);
						Assert.AreEqual("ProductTypeDTO", field.RelatedTypeName);
						break;
				}
			}
		}

		[Test]
		public void CanReadHasAndBelongsToManyDTOPropertiesWithConventions()
		{
			IPropertyReaderService<AccessoryDTO> propertyReaderService = new PropertyReaderService<AccessoryDTO>();
			propertyReaderService.ReadViewModelProperties();

			Assert.AreEqual(typeof(AccessoryDTO).GetProperties().Length, propertyReaderService.FormFields.Count);

			foreach (var field in propertyReaderService.FormFields)
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

		[Test]
		public void CanReadSingleAndMultiDropDownListPropertiesWithConfigurations()
		{
			IPropertyReaderService<NewsArticleDTO> propertyReaderService = new PropertyReaderService<NewsArticleDTO>();
			propertyReaderService.ReadViewModelProperties();

			Assert.AreEqual(typeof(NewsArticleDTO).GetProperties().Length, propertyReaderService.FormFields.Count);

			foreach (var field in propertyReaderService.FormFields)
			{
				switch (field.Id)
				{
					case "Id":
						AssertFormFieldData("Id", FieldType.Hidden, field);
						break;
					case "Headline":
						AssertFormFieldData("Headline", FieldType.SingleLineTextField, field);
						break;
					case "Story":
						AssertFormFieldData("Story", FieldType.MultiLineTextField, field);
						break;
					case "Date":
						AssertFormFieldData("Date", FieldType.Date, field);
						break;
					case "Type":
						AssertFormFieldData("Type", FieldType.SingleSelectDropDownList, field);
						Assert.AreEqual("Id", field.OptionValue);
						Assert.AreEqual("Name", field.OptionText);
						break;
					case "Tags":
						AssertFormFieldData("Tags", FieldType.MultiSelectDropDownList, field);
						Assert.AreEqual("Id", field.OptionValue);
						Assert.AreEqual("Tag", field.OptionText);
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

	public class TestSimpleConventionDto : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public DateTime ExpiresOn { get; set; }
		public bool ShowOnHomePage { get; set; }
	}

	public class TestSimpleConfigurationDTO : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		[FormFieldType(FieldType.MultiLineTextField)]
		public string ShortDescription { get; set; }
		[FormFieldType(FieldType.Asset)]
		public string NewsImage { get; set; }
		[FormFieldType(FieldType.Ignore)]
		public string IgnoreMeField { get; set; }
	}

	public class ProductTypeDTO : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}

	public class ProductDTO : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int StockCount { get; set; }
		public ProductTypeDTO Type { get; set; }
	}

	public class AccessoryDTO : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public IList<ProductDTO> Products { get; set; }
	}

	public class NewsTypeDTO : IDto
	{
		public Guid Id { get; set; }
		public bool Archive { get; set; }
		public string Name { get; set; }
	}

	public class NewsTagDTO : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool Archive { get; set; }
		public string Tag { get; set; }
	}

	public class NewsArticleDTO : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		[FormFieldType(FieldType.SingleSelectDropDownList, "Id", "Name")]
		public NewsTypeDTO Type { get; set; }
		[FormFieldType(FieldType.MultiSelectDropDownList, "Id", "Tag")]
		public IList<NewsTagDTO> Tags { get; set; }
		public string Headline { get; set; }
		[FormFieldType(FieldType.MultiLineTextField)]
		public string Story { get; set; }
		public DateTime Date { get; set; }
	}
}