using System.ComponentModel.Composition;
using Castle.MonoRail.Framework;
using Membrane.Commons.Persistence;
using Membrane.Commons.Services;

namespace Membrane.Commons.Editors
{
	[Export(typeof(IEditorController<>))]
	public class BaseCrudController<T> : BaseController, IEditorController<T> where T : IEntity
	{
		private IBaseCrudService<T> service;

		public BaseCrudController(IBaseCrudService<T> service)
		{
			this.service = service;
		}

		public virtual void List()
		{
			PropertyBag["data"] = service.GetAllData();
		}

		public virtual void New()
		{
		}

		public virtual void Edit(int id)
		{
		}

		public virtual void Submit([DataBind("item")] T item)
		{
		}

		public virtual void ConfirmDelete(int id)
		{
		}

		public virtual void Delete(int id)
		{
		}
	}
}