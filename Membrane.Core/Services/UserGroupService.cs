using Membrane.Commons.CRUD.Services;
using Membrane.Commons.Persistence;
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