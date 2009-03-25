using Membrane.Core.DTOs;

namespace Membrane.Core.Services.Interfaces
{
	public interface IAuthenticationService
	{
		AuthenticatedUserDTO AuthenticateUser(string username, string password);
	}
}