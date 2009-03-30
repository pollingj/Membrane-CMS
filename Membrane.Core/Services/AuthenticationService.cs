using System;
using System.Security.Cryptography;
using System.Text;
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

		/// <summary>
		/// Method to Hash any given value.  
		/// </summary>
		/// <param name="valueToHash">What needs hashing?</param>
		/// <returns>Hashed value (string)</returns>
		private string Hash(string valueToHash)
		{
			SHA512 sha = new SHA512Managed();
			var hashed = string.Empty;
			if (valueToHash != null)
			{
				byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(valueToHash));
				hashed = BitConverter.ToString(hash);
			}
			return hashed;
		} 

		public AuthenticatedUserDTO AuthenticateUser(AuthenticationRequestDTO authenticationRequest)
		{
			GuardAgainst.ArgumentNull(authenticationRequest, "authenticationRequest");

			var user = userRepository.FindOne(new UserByUsernameAndPassword(authenticationRequest.Username, Hash(authenticationRequest.Password)));

			return Mapper.Map<MembraneUser, AuthenticatedUserDTO>(user);
		}
	}
}