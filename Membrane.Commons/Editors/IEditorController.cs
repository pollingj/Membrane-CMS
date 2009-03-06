using Membrane.Commons.Persistence;

namespace Membrane.Commons.Editors
{
	public interface IEditorController<T> where T : IEntity
	{
		void List();
		void New();
		void Edit(int id);
		void Submit(T item);
		void ConfirmDelete(int id);
		void Delete(int id);
	}
}