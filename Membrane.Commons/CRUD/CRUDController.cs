using System;
using Castle.Components.Validator;
using Castle.MonoRail.Framework;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.CRUD
{
	[Layout("default"), Rescue("generalerror")]
	public class CRUDController<DTO, Entity> : SmartDispatcherController, ICRUDController<DTO, Entity> 
		where DTO : IDTO 
		where Entity : IEntity
	{
		private readonly ICRUDService<DTO, Entity> service;
		private const int defaultPageNumber = 1;
		private const int defaultPageSize = 10;
		 
		public CRUDController(ICRUDService<DTO, Entity> service)
		{
			this.service = service;	
		}

		public virtual void List()
		{
			List(defaultPageNumber, defaultPageSize);
		}

		public virtual void List(int currentPage, int pageSize)
		{
			PropertyBag["items"] = service.GetPagedItems(currentPage, pageSize);
		}

		public virtual void New()
		{
			PropertyBag["itemtype"] = typeof(DTO);

			RenderView("Form");
		}

		public virtual void Edit(Guid id)
		{
			PropertyBag["item"] = service.GetItem(id);

			RenderView("Form");
		}

		public virtual void Submit([DataBind("item", Validate = true)] DTO item)
		{
			var submitError = false;

			if (Validator.IsValid(item))
			{
				// Are we handling a new or editted user group?
				if (item.Id == Guid.Empty)
				{
					var newId = service.Create(item);

					if (newId == Guid.Empty)
					{
						submitError = CreateError("There was a problem inserting this item.");
					}
				}
				else
				{
					var updateSuccess = service.Update(item);

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


		public virtual void ConfirmDelete(Guid id)
		{
			PropertyBag["Item"] = service.GetItem(id);
		}

		public virtual void Delete(Guid id)
		{
			if (service.Delete(id))
			{
				RedirectToAction("List");
			}
			else
			{
				CreateError("There was a problem deleting this item");
				Response.RedirectToReferrer();
			}
		}

		private bool CreateError(string errorMessage)
		{
			var errorSummary = new ErrorSummary();
			errorSummary.RegisterErrorMessage(string.Empty, errorMessage);
			Flash["error"] = errorSummary;
			return true;
		}
	}
}