using AutoMapper;
using Membrane.Commons;
using Membrane.Commons.Persistence;
using Membrane.Core.DTOs;
using Membrane.Core.Queries.Users;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;

namespace Membrane.Core.Services
{
	public class AuthenticationService : BaseUserService, IAuthenticationService
	{
		private readonly IRepository<MembraneUser> userRepository;

		public AuthenticationService(IRepository<MembraneUser> userRepository)
		{
			GuardAgainst.ArgumentNull(userRepository, "userRepository");

			this.userRepository = userRepository;
		}

		public AuthenticatedUserDTO AuthenticateUser(AuthenticationRequestDTO authenticationRequest)
		{
			GuardAgainst.ArgumentNull(authenticationRequest, "authenticationRequest");

			var user = userRepository.FindOne(new UserByUsernameAndPassword(authenticationRequest.Username, Hash(authenticationRequest.Password)));

			return Mapper.Map<MembraneUser, AuthenticatedUserDTO>(user);
		}
	}
}