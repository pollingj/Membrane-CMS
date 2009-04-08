using System;
using System.Collections.Generic;

namespace Membrane.Commons.CRUD.Services
{
	public interface ICRUDService<DTO, Entity>
	{
		void RegisterMappings();
		IList<DTO> GetPagedItems(int currentPage, int pageSize);
		DTO GetItem(Guid id);
		Guid Create(DTO group);
		bool Update(DTO group);
		bool Delete(Guid id);
	}
}