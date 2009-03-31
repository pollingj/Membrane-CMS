using System.Collections.Generic;
using Membrane.Core.DTOs;

namespace Membrane.Core.Services.Interfaces
{
	public interface IUserGroupService
	{
		IList<UserGroupDTO> GetPagedUserGroups(int currentPage, int pageSize);
	}
}