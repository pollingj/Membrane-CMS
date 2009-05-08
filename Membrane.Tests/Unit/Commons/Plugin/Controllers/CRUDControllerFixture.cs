using System;
using System.Collections.Generic;
using Membrane.Commons.FormGeneration;
using Membrane.Commons.FormGeneration.Enums;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.Controllers;
using Membrane.Commons.Plugin.DTOs.Interfaces;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Tests.Unit.Web.MonoRail.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Commons.Plugin.Controllers
{
	[TestFixture]
	public class CRUDControllerFixture<TDto, TEntity> : BaseControllerFixture
		where TDto : IDto
		where TEntity : IEntity
	{


		public CRUDController<TDto, TEntity> Controller { get; set; }
		public ICRUDService<TDto, TEntity> Service { get; set; }
		public IPropertyReaderService<TDto> PropertyReaderService { get; set; }

		private const int defaultCurrentPageNumber = 1;
		private const int defaultCurrentPageSize = 10;
		private const int anotherPageNumber = 2;
		private const int anotherPageSize = 20;

		protected string ListView = "/Shared/List";

		public IList<TDto> ListDTO { get; set; }
		public TDto NewDTO { get; set; }
		public TDto InvalidDTO { get; set; }
		public TDto EditDTO { get; set; }
		public TDto DeleteDTO { get; set; }
		public IList<FormField> FormFields { get; set; }


		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();

			Service = mockery.DynamicMock<ICRUDService<TDto, TEntity>>();
			PropertyReaderService = mockery.DynamicMock<IPropertyReaderService<TDto>>();



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
								Expect.Call(() => PropertyReaderService.ReadViewModelProperties());
								Expect.Call(PropertyReaderService.FormFields).Return(FormFields);
				           	})
				.Verify(() => Controller.New());

			Assert.AreEqual(FormFields, Controller.PropertyBag["fields"]);
			Assert.AreEqual(typeof(TDto), Controller.PropertyBag["itemtype"]);
			Assert.AreEqual("/Shared/Form", Controller.SelectedViewName);
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
								Expect.Call(() => PropertyReaderService.ReadViewModelProperties());
								Expect.Call(PropertyReaderService.FormFields).Return(FormFields);
				           	})
				.Verify(() => Controller.Edit(EditDTO.Id));

			Assert.AreEqual(FormFields, Controller.PropertyBag["fields"]);
			Assert.AreEqual(EditDTO, Controller.PropertyBag["item"]);
			Assert.AreEqual("/Shared/Form", Controller.SelectedViewName);
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
			Assert.AreEqual("/Shared/ConfirmDelete", Controller.SelectedViewName);
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
			Assert.AreEqual("/Controller/List.castle", Response.RedirectedTo);
		}

		private void AssertSubmitFailure(TDto failedDTO)
		{
			Assert.AreEqual(failedDTO, Controller.Flash["item"]);
			Assert.IsNotNull(Controller.Flash["error"]);
			Assert.AreEqual(@"Controller\Action", Controller.SelectedViewName);
		}


		private void AssertListData()
		{
			Assert.AreEqual(ListView, Controller.SelectedViewName, "List view not being used");
			Assert.AreEqual(ListDTO, Controller.Flash["items"], "groups PropertyBag not being set");
		}
	}
}