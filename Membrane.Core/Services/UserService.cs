using System;
using AutoMapper;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.Exceptions;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;

namespace Membrane.Core.Services
{
	public class UserService : BaseUserService, IUserService
	{
		private	IRepository<MembraneUser> repository;

		public UserService(IRepository<MembraneUser> repository)
		{
			this.repository = repository;
		}

		public UserDetailsResponseDTO LoadDetails(Guid id)
		{
			var user = repository.FindById(id);

			return Mapper.Map<MembraneUser, UserDetailsResponseDTO>(user);
		}

		public bool UpdateDetails(UserDetailsRequestDTO details)
		{
			var success = true;
			try
			{
				if (details.Password != string.Empty)
				{
					details.Password = Hash(details.Password);
				}
				var user = Mapper.Map<UserDetailsRequestDTO, MembraneUser>(details);
				repository.Update(user);
			}
			catch (RepositoryUpdateException)
			{
				success = false;
			}

			return success;
		}
	}
}