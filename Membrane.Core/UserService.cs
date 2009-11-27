using System;
using System.Security.Cryptography;
using System.Text;
using Membrane.Models.UserManagement;
using Membrane.Services.Interfaces;
using Query;
using Rhino.Commons;

namespace Membrane.Services
{
	public class UserService : IUserService
	{
		#region Constructors

		private IRepository<User> userRepository;

		public UserService():this(IoC.Resolve<IRepository<User>>())
		{
		}

		public UserService(IRepository<User> userRepository)
		{
			this.userRepository = userRepository;
		}

		#endregion
		#region Private Methods

		public string Hash(string value)
		{
			SHA512 hasher = SHA512.Create();
			byte[] valueBytes = Encoding.UTF8.GetBytes(value);
			byte[] hashedBytes = hasher.ComputeHash(valueBytes);
			return Convert.ToBase64String(hashedBytes);
		}

		#endregion

		public User AuthouriseUser(string username, string password)
		{
			User user = userRepository.FindOne(Where.User.Username.Eq(username) && Where.User.Password.Eq(Hash(password)));
			return user;
		}

		public bool LogoutUser(int userid)
		{
			throw new System.NotImplementedException();
		}
	}
}