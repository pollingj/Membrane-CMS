using System;
using Membrane.Commons;
using Membrane.Commons.Persistence;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;

namespace Membrane.Core.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IRepository<User> userRepository;

		public AuthenticationService(IRepository<User> userRepository)
		{
			GuardAgainst.ArgumentNull(userRepository, "userRepository");

			this.userRepository = userRepository;
		}

		public AuthenticatedUserDTO AuthenticateUser(AuthenticationRequestDTO authenticationRequest)
		{
			throw new NotImplementedException();
		}
	}
}