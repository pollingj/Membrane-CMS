
using System;
using System.Collections.Generic;
using AutoMapper;
using Membrane.Commons.CRUD.Queries;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.Exceptions;

namespace Membrane.Commons.CRUD.Services
{
	/// <summary>
	/// Basic CRUDService that is used as standard in the CRUDController.  It can be overwritten if required.
	/// </summary>
	/// <typeparam name="TDto">The DTO type</typeparam>
	/// <typeparam name="TEntity">The Entity type</typeparam>
	public class CRUDService<TDto, TEntity> : ICRUDService<TDto, TEntity> 
		where TDto : IDto
		where TEntity : IEntity
	{

		private readonly IRepository<TEntity> repository;

		public CRUDService(IRepository<TEntity> repository)
		{
			this.repository = repository;
			RegisterMappings();
		}

		/// <summary>
		/// Does two basic mappings
		///  - TEntity -> TDto
		///  - TDto -> TEntity
		/// 
		/// Can be overwritten for more specific mappings
		/// </summary>
		public virtual void RegisterMappings()
		{
			Mapper.CreateMap<TEntity, TDto>();
			Mapper.CreateMap<TDto, TEntity>();
		}

		/// <summary>
		/// Get the Items using Pagination
		/// </summary>
		/// <param name="currentPage">The page number</param>
		/// <param name="pageSize">The size of a page</param>
		/// <returns>A List of Dto objects</returns>
		public virtual IList<TDto> GetPagedItems(int currentPage, int pageSize)
		{
			var skip = 0;

			if (currentPage > 1)
				skip = pageSize * (currentPage - 1);

			var items = repository.Find(new PagedItems<TEntity>(skip, pageSize));

			return Mapper.Map<ICollection<TEntity>, IList<TDto>>(items);
		}

		public IList<TDto> GetItems()
		{
			var items = repository.Find(new QueryItems<TEntity>());

			return Mapper.Map<ICollection<TEntity>, IList<TDto>>(items);
		}

		/// <summary>
		/// Gets an individual item
		/// </summary>
		/// <param name="id">The id of the item</param>
		/// <returns>The item</returns>
		public virtual TDto GetItem(Guid id)
		{
			var group = repository.FindById(id);

			return Mapper.Map<TEntity, TDto>(group);
		}

		/// <summary>
		/// Creates (saves) a new item
		/// </summary>
		/// <param name="item">The item that needs to be saved</param>
		/// <returns>The Guid of the new object</returns>
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

		/// <summary>
		/// Updates an item
		/// </summary>
		/// <param name="item">The item that needs to be updated</param>
		/// <returns>Has the item been updated successfully?</returns>
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

		/// <summary>
		/// Deletes an item
		/// </summary>
		/// <param name="id">The id of the item that needs to be deleted</param>
		/// <returns>Has the item been deleted successfully?</returns>
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