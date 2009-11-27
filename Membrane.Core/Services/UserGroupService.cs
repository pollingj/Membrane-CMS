using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.Services;
using Membrane.Core.DTOs;
using Membrane.Entities;

namespace Membrane.Core.Services
{
	public class UserGroupService : CRUDService<UserGroupDTO, UserGroup>
	{
		public UserGroupService(IRepository<UserGroup> repository) : base(repository)
		{
		}
	}

}