using System.Linq;
using Membrane.Commons.Persistence;
using Membrane.Entities;

namespace Membrane.Core.Queries.UserGroups
{
	public class PagedUserGroups : IQueryCommand<UserGroup>
	{
		private readonly int skip;
		private readonly int take;

		public PagedUserGroups(int skip, int take)
		{
			this.skip = skip;
			this.take = take;
		}

		public IQueryable<UserGroup> Execute(IRepository<UserGroup> repository)
		{
			return repository.AsQueryable().Skip(skip).Take(take);
		}
	}
}