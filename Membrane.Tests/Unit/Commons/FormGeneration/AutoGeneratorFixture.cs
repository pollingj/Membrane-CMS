using System;
using Membrane.Commons.CRUD;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Interfaces;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Commons.FormGeneration
{
	[TestFixture]
	public class AutoGeneratorFixture
	{
		private IAutoGenerator<TestSimpleConventionDTO> autoGenerator;

		[SetUp]
		public void SetUp()
		{
			autoGenerator = new AutoGenerator<TestSimpleConventionDTO>();
		}

		[Test]
		public void CanReadSimpleConventionBasedDTOProperties()
		{
			autoGenerator.ReadViewModelProperties();

			Assert.AreEqual(typeof (TestSimpleConventionDTO).GetProperties().Length, autoGenerator.FormFields.Count);

			foreach (var field in autoGenerator.FormFields)
			{
				switch (field.Id)
				{
					case "item.Id":
						Assert.AreEqual("Id", field.Label);
						Assert.AreEqual(FieldType.Hidden, field.Type);
						break;
					case "item.ProductName":
						Assert.AreEqual("Product Name", field.Label);
						Assert.AreEqual(FieldType.SingleLineTextField, field.Type);
						break;
					case "item.Price":
						Assert.AreEqual("Price", field.Label);
						Assert.AreEqual(FieldType.SingleLineTextField, field.Type);
						break;
					case "item.ExpiresOn":
						Assert.AreEqual("Expires On", field.Label);
						Assert.AreEqual(FieldType.Date, field.Type);
						break;
					case "item.ShowOnHomePage":
						Assert.AreEqual("Show On Home Page", field.Label);
						Assert.AreEqual(FieldType.CheckBox, field.Type);
						break;
				}

			}
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
}