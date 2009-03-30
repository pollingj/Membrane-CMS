using System;

namespace Membrane.Commons
{
	public interface IMembraneController<T>
	{
		void List();
		void List(int currentPage, int pageSize);
		void New();
		void Edit(Guid itemId);
		void Submit(T item);
		void ConfirmDelete(Guid itemId);
		void Delete(Guid itemId);
	}
}