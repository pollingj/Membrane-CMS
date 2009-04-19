using System;
using System.Collections.Generic;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Interfaces;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.Services;
using Membrane.Tests.Unit.Web.MonoRail.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Commons.Plugin.Controllers
{
	[TestFixture]
	public class CRUDControllerFixture<DTO, Entity> : BaseControllerFixture
		where DTO : IDto
		where Entity : IEntity
	{


		public CRUDController<DTO, Entity> Controller { get; set; }
		public ICRUDService<DTO, Entity> Service { get; set; }
		public IAutoGenerator<DTO> AutoGenerator { get; set; }

		private const int defaultCurrentPageNumber = 1;
		private const int defaultCurrentPageSize = 10;
		private const int anotherPageNumber = 2;
		private const int anotherPageSize = 20;

		public IList<DTO> ListDTO { private get; set; }
		public DTO NewDTO { private get; set; }
		public DTO InvalidDTO { private get; set; }
		public DTO EditDTO { private get; set; }
		public DTO DeleteDTO { private get; set; }
		public IList<FormField> FormFields { private get; set; }

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			Service = mockery.DynamicMock<ICRUDService<DTO, Entity>>();
			AutoGenerator = mockery.DynamicMock<IAutoGenerator<DTO>>();



			FormFields = new List<FormField>
			             	{
			             		new FormField {Id = "Id", Label = "Id", Type = FieldType.Hidden},
			             		new FormField {Id = "ProductName", Label = "Product Name", Type = FieldType.SingleLineTextField},
			             		new FormField {Id = "Price", Label = "Price", Type = FieldType.SingleLineTextField},
			             		new FormField { Id = "Description", Label = "Description", Type = FieldType.MultiLineTextField}
			             	};

		}

		[Test]
		public virtual void CanListItemsWithNoPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(Service.GetPagedItems(defaultCurrentPageNumber, defaultCurrentPageSize)).Return(ListDTO))
				.Verify(() => Controller.List());

			AssertListData();
		}


		[Test]
		public virtual void CanListItemsWithPagingInformation()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(Service.GetPagedItems(anotherPageNumber, anotherPageSize)).Return(ListDTO))
				.Verify(() => Controller.List(anotherPageNumber, anotherPageSize));

			AssertListData();
		}

		[Test]
		public virtual void CanShowNewItemPage()
		{

			With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(() => AutoGenerator.ReadViewModelProperties());
				           		Expect.Call(AutoGenerator.FormFields).Return(FormFields);
				           	})
				.Verify(() => Controller.New());

			Assert.AreEqual(FormFields, Controller.PropertyBag["fields"]);
			Assert.AreEqual(typeof(DTO), Controller.PropertyBag["itemtype"]);
			Assert.AreEqual(@"\Shared\Form", Controller.SelectedViewName);
		}

		[Test]
		public virtual void CanSuccessfullySubmitNewItem()
		{
			NewDTOSubmission(Guid.NewGuid());

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public virtual void CanFailValidationOnSubmit()
		{
			Controller.Submit(InvalidDTO);

			AssertSubmitFailure(InvalidDTO);
		}

		[Test]
		public virtual void CanFailSubmitItem()
		{
			NewDTOSubmission(Guid.Empty);

			AssertSubmitFailure(NewDTO);
		}

		[Test]
		public virtual void CanShowEditItemPage()
		{
			With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(Service.GetItem(EditDTO.Id)).Return(EditDTO);
				           		Expect.Call(() => AutoGenerator.ReadViewModelProperties());
				           		Expect.Call(AutoGenerator.FormFields).Return(FormFields);
				           	})
				.Verify(() => Controller.Edit(EditDTO.Id));

			Assert.AreEqual(FormFields, Controller.PropertyBag["fields"]);
			Assert.AreEqual(EditDTO, Controller.PropertyBag["item"]);
			Assert.AreEqual(@"\Shared\Form", Controller.SelectedViewName);
		}

		[Test]
		public virtual void CanSuccessfullySubmitEdittedItem()
		{
			EditDTOSubmission(true);

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public virtual void CanFailSubmitEdittedItem()
		{
			EditDTOSubmission(false);

			AssertSubmitFailure(EditDTO);
		}

		[Test]
		public virtual void CanShowItemForConfirmDelete()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(Service.GetItem(DeleteDTO.Id)).Return(DeleteDTO))
				.Verify(() => Controller.ConfirmDelete(DeleteDTO.Id));

			Assert.AreEqual(DeleteDTO, Controller.PropertyBag["item"]);
			Assert.AreEqual(@"\Shared\ConfirmDelete", Controller.SelectedViewName);
		}

		[Test]
		public virtual void CanSuccessfullyDeleteItem()
		{
			MockDeleteItem(true);

			AssertSuccessfulActionAndRedirectedBackToList();
		}

		[Test]
		public virtual void CanFailDeletingItem()
		{
			MockDeleteItem(false);

			Assert.IsNotNull(Controller.Flash["error"]);
		}

		private void MockDeleteItem(bool deleteSuccess)
		{
			var groupId = Guid.NewGuid();

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(Service.Delete(groupId)).Return(deleteSuccess))
				.Verify(() => Controller.Delete(groupId));
		}

		private void NewDTOSubmission(Guid id)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(Service.Create(NewDTO)).Return(id))
				.Verify(() => Controller.Submit(NewDTO));
		}

		private void EditDTOSubmission(bool updateSuccess)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(Service.Update(EditDTO)).Return(updateSuccess))
				.Verify(() => Controller.Submit(EditDTO));
		}

		private void AssertSuccessfulActionAndRedirectedBackToList()
		{
			Assert.IsNull(Controller.Flash["error"]);
			Assert.AreEqual(@"/Controller/List.castle", Response.RedirectedTo);
		}

		private void AssertSubmitFailure(DTO failedDTO)
		{
			Assert.AreEqual(failedDTO, Controller.Flash["item"]);
			Assert.IsNotNull(Controller.Flash["error"]);
			Assert.AreEqual(@"Controller\Action", Controller.SelectedViewName);
		}


		private void AssertListData()
		{
			Assert.AreEqual(@"\Shared\List", Controller.SelectedViewName, "List view not being used");
			Assert.AreEqual(ListDTO, Controller.PropertyBag["items"], "groups PropertyBag not being set");
		}
	}
}