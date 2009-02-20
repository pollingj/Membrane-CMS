using Membrane.Models.UserManagement;

namespace Membrane.Services.Interfaces
{
	public interface IUserService
	{
		User AuthouriseUser(string username, string password);
		bool LogoutUser(int userid);
	}
}