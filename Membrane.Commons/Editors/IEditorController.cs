using Membrane.Commons.Persistence;

namespace Membrane.Commons.Editors
{
	public interface IEditorController<T> where T : IEntity
	{
		void List(int currentPage, int displayCount);
		void New(int currentPage, int displayCount);
		void Edit(int id, int currentPage, int displayCount);
		void Submit(T item, int currentPage, int displayCount);
		void ConfirmDelete(int id, int currentPage, int displayCount);
		void Delete(int id, int currentPage, int displayCount);
	}
}