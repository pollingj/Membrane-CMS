using System;
using Castle.Components.Validator;
using Castle.MonoRail.Framework;
using Membrane.Commons.FormGeneration.Services.Interfaces;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.Services;

namespace Membrane.Commons.Plugin.Controllers
{
	/// <summary>
	/// Basic CRUDController that is inherited by the other editor controllers.  It can be overwritten if required.
	/// </summary>
	/// <typeparam name="TDto">The DTO type</typeparam>
	/// <typeparam name="TEntity">The Entity type</typeparam>
	[Layout("default"), Rescue("generalerror")]
	public class CRUDController<TDto, TEntity> : SmartDispatcherController, ICRUDController<TDto, TEntity> 
		where TDto : IDto 
		where TEntity : IEntity
	{
		public ICRUDService<TDto, TEntity> Service { get; set; }
		private readonly IPropertyReaderService<TDto> propertyReaderService;
		private const int defaultPageNumber = 1;
		private const int defaultPageSize = 10;

		public CRUDController(ICRUDService<TDto, TEntity> service, IPropertyReaderService<TDto> propertyReaderService)
		{
			Service = service;
			this.propertyReaderService = propertyReaderService;
		}

		/// <summary>
		/// The basic List action (no paging information supplied)
		/// </summary>
		public virtual void List()
		{
			List(defaultPageNumber, defaultPageSize);
		}

		/// <summary>
		/// The paging List action
		/// </summary>
		/// <param name="currentPage">The current page number</param>
		/// <param name="pageSize">The page size</param>
		public virtual void List(int currentPage, int pageSize)
		{
			Flash["items"] = Service.GetPagedItems(currentPage, pageSize);

			RenderView(@"\Shared\List");
		}

		/// <summary>
		/// The New action for when a new item is required
		/// </summary>
		public virtual void New()
		{
			PropertyBag["itemtype"] = typeof(TDto);
			GetFormFields();
			RenderView(@"\Shared\Form");
		}

		/// <summary>
		/// The Edit action
		/// </summary>
		/// <param name="id">The id of the item to be edited</param>
		public virtual void Edit(Guid id)
		{
			PropertyBag["item"] = Service.GetItem(id);
			GetFormFields();
			RenderView(@"\Shared\Form");
		}

		/// <summary>
		/// The Submit action (both New and Edit forms post to this)
		/// </summary>
		/// <param name="item">The item that is posted</param>
		[AccessibleThrough(Verb.Post)]
		public virtual void Submit([DataBind("item", Validate = true)] TDto item)
		{
			var submitError = false;

			if (Validator.IsValid(item))
			{
				// Are we handling a new or editted user group?
				if (item.Id == Guid.Empty)
				{
					var newId = Service.Create(item);

					if (newId == Guid.Empty)
					{
						submitError = CreateError("There was a problem inserting this item.");
					}
				}
				else
				{
					var updateSuccess = Service.Update(item);

					if (!updateSuccess)
					{
						submitError = CreateError("There was a problem updating this item.");
					}
				}
			}
			else
			{
				Flash["error"] = Validator.GetErrorSummary(item);
				submitError = true;
			}

			if (submitError)
			{
				Flash["item"] = item;
				RedirectToReferrer();
			}
			else
			{
				RedirectToAction("List");
			}

		}

		/// <summary>
		/// Action to confirm is a item should be deleted or not
		/// </summary>
		/// <param name="id">The id of the item</param>
		public virtual void ConfirmDelete(Guid id)
		{
			PropertyBag["Item"] = Service.GetItem(id);

			RenderView(@"\Shared\ConfirmDelete");
		}

		/// <summary>
		/// The Delete action
		/// </summary>
		/// <param name="id">The id of the item to be deleted</param>
		public virtual void Delete(Guid id)
		{
			if (Service.Delete(id))
			{
				RedirectToAction("List");
			}
			else
			{
				CreateError("There was a problem deleting this item");
				Response.RedirectToReferrer();
			}
		}

		public virtual void LoadSupportiveData()
		{

		}

		
		protected bool CreateError(string errorMessage)
		{
			var errorSummary = new ErrorSummary();
			errorSummary.RegisterErrorMessage(string.Empty, errorMessage);
			Flash["error"] = errorSummary;
			return true;
		}

		private void GetFormFields()
		{
			propertyReaderService.ReadViewModelProperties();
			PropertyBag["fields"] = propertyReaderService.FormFields;
			LoadSupportiveData();
		}
	}
}