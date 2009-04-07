
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
		}

		public IList<DTO> GetPagedItems(int currentPage, int pageSize)
		{
			var skip = 0;

			if (currentPage > 1)
				skip = pageSize * (currentPage - 1);

			var items = repository.Find(new PagedItems<Entity>(skip, pageSize));

			return Mapper.Map<ICollection<Entity>, IList<DTO>>(items);
		}

		public DTO GetItem(Guid id)
		{
			var group = repository.FindById(id);

			return Mapper.Map<Entity, DTO>(group);
		}

		public Guid Create(DTO item)
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

		public bool Update(DTO item)
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

		public bool Delete(Guid id)
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

		/*
		 * 
		 * 		public IList<UserGroupDTO> GetPagedUserGroups(int currentPage, int pageSize)
		{
			var skip = 0;

			if (currentPage > 1)
				skip = pageSize*(currentPage - 1);

			var groups = userGroupRepository.Find(new PagedUserGroups(skip, pageSize));

			return Mapper.Map<ICollection<UserGroup>, IList<UserGroupDTO>>(groups);
		}

		public Guid Create(UserGroupDTO group)
		{
			var id = group.Id = Guid.NewGuid();

			try
			{
				userGroupRepository.Save(Mapper.Map<UserGroupDTO, UserGroup>(group));
			}
			catch(RepositorySaveException)
			{
				id = Guid.Empty;
			}

			return id;
		}

		public UserGroupDTO GetUserGroup(Guid id)
		{
			var group = userGroupRepository.FindById(id);

			return Mapper.Map<UserGroup, UserGroupDTO>(group);
		}

		public bool Update(UserGroupDTO group)
		{
			var success = true;
			try
			{
				userGroupRepository.Update(Mapper.Map<UserGroupDTO, UserGroup>(group));
			}
			catch(RepositoryUpdateException)
			{
				success = false;
			}

			return success;
		}

		public bool Delete(Guid id)
		{
			var success = true;

			try
			{
				userGroupRepository.Delete(id);
			}
			catch(RepositoryDeleteException)
			{
				success = false;
			}

			return success;
		}
	}*/
	}
}