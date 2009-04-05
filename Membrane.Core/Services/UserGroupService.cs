using System;
using System.Collections.Generic;
using AutoMapper;
using Membrane.Commons.Persistence;
using Membrane.Core.DTOs;
using Membrane.Core.Exceptions;
using Membrane.Core.Queries.UserGroups;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;

namespace Membrane.Core.Services
{
	public class UserGroupService : IUserGroupService
	{
		private IRepository<UserGroup> userGroupRepository;

		public UserGroupService(IRepository<UserGroup> userGroupRepository)
		{
			this.userGroupRepository = userGroupRepository;
		}

		public ICollection<UserGroupDTO> GetPagedUserGroups(int currentPage, int pageSize)
		{
			var skip = 0;

			if (currentPage > 1)
				skip = pageSize*(currentPage - 1);

			var groups = userGroupRepository.Find(new PagedUserGroups(skip, pageSize));

			return Mapper.Map<ICollection<UserGroup>, ICollection<UserGroupDTO>>(groups);
		}

		public Guid Create(UserGroupDTO group)
		{
			var id = group.Id = Guid.NewGuid();

			try
			{
				userGroupRepository.Save(Mapper.Map<UserGroupDTO, UserGroup>(group));
			}
			catch(NHibernateSaveException)
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
			throw new System.NotImplementedException();
		}

		public bool Delete(Guid id)
		{
			throw new System.NotImplementedException();
		}
	}
}