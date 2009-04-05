using System;
using System.Collections.Generic;
using Membrane.Core.DTOs;

namespace Membrane.Core.Services.Interfaces
{
	public interface IUserGroupService
	{
		ICollection<UserGroupDTO> GetPagedUserGroups(int currentPage, int pageSize);
		Guid Create(UserGroupDTO group);
		UserGroupDTO GetUserGroup(Guid id);
		bool Update(UserGroupDTO group);
		bool Delete(Guid id);
	}
}