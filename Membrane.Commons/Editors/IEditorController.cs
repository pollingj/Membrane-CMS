using System;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.Editors
{
	public interface IEditorController<T> where T : IEntity
	{
		void List(int currentPage, int displayCount);
		void New(int currentPage, int displayCount);
		void Edit(Guid id, int currentPage, int displayCount);
		void Submit(T item, int currentPage, int displayCount);
		void ConfirmDelete(Guid id, int currentPage, int displayCount);
		void Delete(Guid id, int currentPage, int displayCount);

		void LoadSupportiveData();
	}
}