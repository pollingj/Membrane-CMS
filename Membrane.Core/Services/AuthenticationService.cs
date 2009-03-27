using System;
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
		private readonly IRepository<MembraneUser> userRepository;

		public AuthenticationService(IRepository<MembraneUser> userRepository)
		{
			GuardAgainst.ArgumentNull(userRepository, "userRepository");

			this.userRepository = userRepository;
		}

		public AuthenticatedUserDTO AuthenticateUser(AuthenticationRequestDTO authenticationRequest)
		{
			GuardAgainst.ArgumentNull(authenticationRequest, "authenticationRequest");

			var user = userRepository.FindOne(new UserByUsernameAndPassword(authenticationRequest.Username, authenticationRequest.Password));
			
			Mapper.CreateMap<MembraneUser, AuthenticatedUserDTO>()
				.ForMember(dest => dest.Type, opt => opt.MapFrom(x => x.Type))
				.ForMember(dest => dest.Identity, opt => opt.Ignore())
				.ForMember(dest => dest.Name, opt => opt.Ignore())
				.ForMember(dest => dest.AuthenticationType, opt => opt.Ignore())
				.ForMember(dest => dest.IsAuthenticated, opt => opt.Ignore());

			return Mapper.Map<MembraneUser, AuthenticatedUserDTO>(user);
		}
	}
}