
using System;
using System.Collections.Generic;
using AutoMapper;
using Membrane.Commons.CRUD.Queries;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.Exceptions;

namespace Membrane.Commons.CRUD.Services
{
	public class CRUDService<TDto, TEntity> : ICRUDService<TDto, TEntity> 
		where TDto : IDTO
		where TEntity : IEntity
	{

		private readonly IRepository<TEntity> repository;

		public CRUDService(IRepository<TEntity> repository)
		{
			this.repository = repository;
			RegisterMappings();
		}

		public virtual void RegisterMappings()
		{
			Mapper.CreateMap<TEntity, TDto>();
			Mapper.CreateMap<TDto, TEntity>();
		}

		public virtual IList<TDto> GetPagedItems(int currentPage, int pageSize)
		{
			var skip = 0;

			if (currentPage > 1)
				skip = pageSize * (currentPage - 1);

			var items = repository.Find(new PagedItems<TEntity>(skip, pageSize));

			return Mapper.Map<ICollection<TEntity>, IList<TDto>>(items);
		}

		public virtual TDto GetItem(Guid id)
		{
			var group = repository.FindById(id);

			return Mapper.Map<TEntity, TDto>(group);
		}

		public virtual Guid Create(TDto item)
		{
			var id = item.Id = Guid.NewGuid();

			try
			{
				repository.Save(Mapper.Map<TDto, TEntity>(item));
			}
			catch (RepositorySaveException)
			{
				id = Guid.Empty;
			}

			return id;
		}

		public virtual bool Update(TDto item)
		{
			var success = true;
			try
			{
				repository.Update(Mapper.Map<TDto, TEntity>(item));
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