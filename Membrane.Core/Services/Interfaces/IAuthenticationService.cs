using Membrane.Core.DTOs;

namespace Membrane.Core.Services.Interfaces
{
	public interface IAuthenticationService : IBaseUserService
	{
		AuthenticatedUserDTO AuthenticateUser(AuthenticationRequestDTO authenticationRequest);
	}
}