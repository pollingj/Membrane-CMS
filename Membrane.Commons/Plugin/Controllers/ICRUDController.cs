using System;

namespace Membrane.Commons.Plugin.Controllers
{
	public interface ICRUDController<DTO, Entity>
	{
		void List();
		void List(int currentPage, int pageSize);
		void New();
		void Edit(Guid id);
		void Submit(DTO item);
		void ConfirmDelete(Guid id);
		void Delete(Guid id);
		void LoadSupportiveData();
	}
}