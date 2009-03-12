using System;
using Castle.Components.Validator;
using Castle.MonoRail.Framework;
using Membrane.Commons.Persistence;
using Membrane.Commons.Services;

namespace Membrane.Commons.Editors
{
	public class BaseCrudController<T> : BaseController, IEditorController<T> where T : IEntity
	{
		protected IBaseCrudService<T> service;

		private const int defaultPage = 1;
		private const int defaultDisplayCount = 10;

		public BaseCrudController(IBaseCrudService<T> service)
		{
			this.service = service;
		}

		private void StorePagingValues(int currentPage, int displayCount)
		{
			PropertyBag["currentPage"] = currentPage;
			PropertyBag["displayCount"] = displayCount;
		}

		private bool CreateCustomError(string message)
		{
			var submitError = true;
			var errorSummary = new ErrorSummary();
			errorSummary.RegisterErrorMessage(string.Empty, message);
			Flash["summary"] = errorSummary;
			return submitError;
		}

		/// <summary>
		/// Default List Action to show first page of results
		/// </summary>
		public void List()
		{
			List(defaultPage, defaultDisplayCount);
		}

		public virtual void List(int currentPage, int displayCount)
		{
			PropertyBag["items"] = service.GetPagedData(currentPage, displayCount);
			StorePagingValues(currentPage, displayCount);

			RenderView(@"\Shared\List");
		}

		public virtual void New(int currentPage, int displayCount)
		{
			PropertyBag["itemtype"] = typeof (T);
			StorePagingValues(currentPage, displayCount);
			RenderView(@"\Shared\Form");
		}

		public virtual void Edit(int id, int currentPage, int displayCount)
		{
			PropertyBag["item"] = service.GetItem(id);
			StorePagingValues(currentPage, displayCount);
			RenderView(@"\Shared\Form");
		}

		public virtual void Submit([DataBind("item")] T item, int currentPage, int displayCount)
		{
			// TODO: Need to add in automatic file upload handling (already have something for this from previous projects)
			var submitError = false;
			if (HasValidationError(item))
			{
				Flash["summary"] = GetErrorSummary(item);
				submitError = true;
			}
			else
			{
				// Inserts
				if (item.Id == Guid.Empty)
				{
					var fromDb = service.Create(item);

					if (fromDb == null)
						submitError = CreateCustomError("There was a problem inserting this item.");
				}
				// Edits
				else
				{
					var success = service.Update(item);

					if (!success)
						submitError = CreateCustomError("There was a problem inserting this item.");
				}
				
			}

			if (submitError)
			{
				Flash["item"] = item;
				RedirectToReferrer();
			}
			else
			{
				RedirectToAction("List", new { currentPage, displayCount });	
			}
			
		}



		public virtual void ConfirmDelete(int id, int currentPage, int displayCount)
		{
			PropertyBag["item"] = service.GetItem(id);
			StorePagingValues(currentPage, displayCount);
			RenderView(@"\Shared\ConfirmDelete");
		}

		public virtual void Delete(int id, int currentPage, int displayCount)
		{
			var success = service.Delete(id);

			if (success)
				RedirectToAction("List");
			else
			{
				CreateCustomError("There was a problem deleting this item");
				RedirectToReferrer();
			}
		}
	}
}