
using System;
using System.Collections.Generic;
using AutoMapper;
using Membrane.Commons.CRUD.Queries;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.Exceptions;

namespace Membrane.Commons.CRUD.Services
{
	public class CRUDService<DTO, Entity> : ICRUDService<DTO, Entity> 
		where DTO : IDTO
		where Entity : IEntity
	{

		private readonly IRepository<Entity> repository;

		public CRUDService(IRepository<Entity> repository)
		{
			this.repository = repository;
			RegisterMappings();
		}

		public virtual void RegisterMappings()
		{
			Mapper.CreateMap<Entity, DTO>();
			Mapper.CreateMap<DTO, Entity>();
		}

		public virtual IList<DTO> GetPagedItems(int currentPage, int pageSize)
		{
			var skip = 0;

			if (currentPage > 1)
				skip = pageSize * (currentPage - 1);

			var items = repository.Find(new PagedItems<Entity>(skip, pageSize));

			return Mapper.Map<ICollection<Entity>, IList<DTO>>(items);
		}

		public virtual DTO GetItem(Guid id)
		{
			var group = repository.FindById(id);

			return Mapper.Map<Entity, DTO>(group);
		}

		public virtual Guid Create(DTO item)
		{
			var id = item.Id = Guid.NewGuid();

			try
			{
				repository.Save(Mapper.Map<DTO, Entity>(item));
			}
			catch (RepositorySaveException)
			{
				id = Guid.Empty;
			}

			return id;
		}

		public virtual bool Update(DTO item)
		{
			var success = true;
			try
			{
				repository.Update(Mapper.Map<DTO, Entity>(item));
			}
			catch (RepositoryUpdateException)
			{
				success = false;
			}

			return success;
		}

		public virtual bool Delete(Guid id)
		{
			var success = true;

			try
			{
				repository.Delete(id);
			}
			catch (RepositoryDeleteException)
			{
				success = false;
			}

			return success;
		}
	}
}