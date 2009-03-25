using Membrane.Core.DTOs;

namespace Membrane.Core.Services.Interfaces
{
	public interface IAuthenticationService
	{
		AuthenticatedUserDTO AuthenticateUser(AuthenticationRequestDTO authenticationRequest);
	}
}