using System;
using System.Collections.Generic;
using Castle.Components.Pagination;
using Membrane.Commons.Editors;
using Membrane.Commons.Persistence;
using Membrane.Commons.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Test.Controllers
{
	public class BaseCrudControllerFixture<T> : BaseControllerFixture where T : IEntity
	{
		public IBaseCrudService<T> service;

		public BaseCrudController<T> controller;

		public const int defaultPage = 1;
		public const int defaultDisplayCount = 10;
		private const int currentPage = 2;
		private const int displayCount = 20;
		private const int itemId = 1;

		protected ICollection<T> items;
		protected IPaginatedPage<T> paginatedData;
		protected T item;
		

		private void AssertPaginatedData(int testCurrentPage, int testDisplayCount)
		{
			Assert.AreEqual(paginatedData, controller.PropertyBag["items"], "Paginated Data");
			CheckPagingPropertyBags(testCurrentPage, testDisplayCount);
			Assert.AreEqual(@"\Shared\List", controller.SelectedViewName, @"Shared\List View");
		}

		private void CheckPagingPropertyBags(int testCurrentPage, int testDisplayCount)
		{
			Assert.AreEqual(testCurrentPage, controller.PropertyBag["currentPage"], "currentPage PropertyBag");
			Assert.AreEqual(testDisplayCount, controller.PropertyBag["displayCount"], "displayCount PropertyBag");
		}

		private void SubmitValidInsertItem(T serviceReturnValue)
		{
			item.Id = Guid.Empty;

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Create(item)).IgnoreArguments().Return(serviceReturnValue))
				.Verify(() => controller.Submit(item, defaultPage, defaultDisplayCount));
		}

		private void SubmitValidEditItem(bool serviceReturnValue)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.Update(item)).IgnoreArguments().Return(serviceReturnValue))
				.Verify(() => controller.Submit(item, defaultPage, defaultDisplayCount));
		}

		[Test]
		public void CanListFirstPageDataWithPagination()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedData(defaultPage, defaultDisplayCount)).Return(paginatedData))
				.Verify(() => controller.List());

			AssertPaginatedData(defaultPage, defaultDisplayCount);
		}
        
		[Test]
		public void CanListAnotherPageDataWithPagination()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetPagedData(currentPage, displayCount)).Return(paginatedData))
				.Verify(() => controller.List(currentPage, displayCount));

			AssertPaginatedData(currentPage, displayCount);
		}

		[Test]
		public void CanDisplayNew()
		{
			controller.New(defaultPage, defaultDisplayCount);

			// Make sure the castle validation stuff is set
			Assert.AreEqual(typeof(T), controller.PropertyBag["itemType"], "itemType PropertyBag for Castle Validation");
			Assert.AreEqual(@"\Shared\Form", controller.SelectedViewName, @"Shared\Form View");
			CheckPagingPropertyBags(defaultPage, defaultDisplayCount);
		}

		[Test]
		public void CanDisplayEdit()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetItem(itemId)).Return(item))
				.Verify(() => controller.Edit(itemId, defaultPage, defaultDisplayCount));

			Assert.IsNotNull(controller.PropertyBag["item"], "item PropertyBag is empty");
			Assert.AreEqual(item, controller.PropertyBag["item"], "item PropertyBag");
			Assert.AreEqual(@"\Shared\Form", controller.SelectedViewName, @"Shared\Form View");
			CheckPagingPropertyBags(defaultPage, defaultDisplayCount);
		}

		[Test]
		public void CanFailValidationOnSubmit()
		{
			SimulateOneValidationErrorFor(controller, item);
			controller.Submit(item, defaultPage, defaultDisplayCount);

			Assert.IsNotNull(controller.Flash["item"]);
			Assert.IsNotNull(controller.Flash["summary"]);
			Assert.AreEqual(item, controller.Flash["item"]);
		}

		[Test]
		public void CanSuccessfullySubmitValidInsertForm()
		{
			SubmitValidInsertItem(item);
		}

		[Test]
		public void CanFailServiceInsertOnSubmit()
		{
			SubmitValidInsertItem(default(T));

			Assert.IsNotNull(controller.Flash["item"]);
			Assert.IsNotNull(controller.Flash["summary"]);
			Assert.AreEqual(item, controller.Flash["item"]);
		}

		[Test]
		public void CanSuccessfullySubmitValidEditForm()
		{
			SubmitValidEditItem(true);
		}

		[Test]
		public void CanFailServiceUpdateOnSubmit()
		{
			SubmitValidEditItem(false);

			Assert.IsNotNull(controller.Flash["item"]);
			Assert.IsNotNull(controller.Flash["summary"]);
			Assert.AreEqual(item, controller.Flash["item"]);
		}
	}
}