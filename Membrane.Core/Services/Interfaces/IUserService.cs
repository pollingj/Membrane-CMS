using System;
using Membrane.Core.DTOs;

namespace Membrane.Core.Services.Interfaces
{
	public interface IUserService : IBaseUserService
	{
		UserDetailsResponseDTO LoadDetails(Guid id);
		bool UpdateDetails(UserDetailsRequestDTO details);
	}
}