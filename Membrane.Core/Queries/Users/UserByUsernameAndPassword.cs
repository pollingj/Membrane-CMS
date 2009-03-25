
using System.Linq;
using Membrane.Commons.Persistence;
using Membrane.Entities;

namespace Membrane.Core.Queries.Users
{
	public class UserByUsernameAndPassword : IQueryCommand<User>
	{
		private readonly string username;
		private readonly string password;

		public UserByUsernameAndPassword(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		public IQueryable<User> Execute(IRepository<User> repository)
		{
			return repository.AsQueryable().Where(a => a.Username == username && a.Password == password);
		}
	}
}