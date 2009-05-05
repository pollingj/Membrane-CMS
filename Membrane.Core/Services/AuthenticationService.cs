using AutoMapper;
using Membrane.Commons;
using Membrane.Commons.Persistence;
using Membrane.Core.DTOs;
using Membrane.Core.Queries.Users;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;

namespace Membrane.Core.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IEncryptionService encryptionService;
		private readonly IRepository<MembraneUser> userRepository;

		public AuthenticationService(IRepository<MembraneUser> userRepository, IEncryptionService encryptionService)
		{
			GuardAgainst.ArgumentNull(userRepository, "userRepository");
			GuardAgainst.ArgumentNull(encryptionService, "encryptionService");

			this.userRepository = userRepository;
			this.encryptionService = encryptionService;
		}

		public AuthenticatedUserDTO AuthenticateUser(AuthenticationRequestDTO authenticationRequest)
		{
			GuardAgainst.ArgumentNull(authenticationRequest, "authenticationRequest");

			var user = userRepository.FindOne(new UserByUsernameAndPassword(authenticationRequest.Username, encryptionService.Encrypt(authenticationRequest.Password)));

			return Mapper.Map<MembraneUser, AuthenticatedUserDTO>(user);
		}
	}
}