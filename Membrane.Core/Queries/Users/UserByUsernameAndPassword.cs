
using System.Linq;
using Membrane.Commons.Persistence;
using Membrane.Entities;

namespace Membrane.Core.Queries.Users
{
	public class UserByUsernameAndPassword : IQueryCommand<MembraneUser>
	{
		private readonly string username;
		private readonly string password;

		public UserByUsernameAndPassword(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		public IQueryable<MembraneUser> Execute(IRepository<MembraneUser> repository)
		{
			return repository.AsQueryable().Where(u => u.Username == username && u.Password == password);
		}
	}
}