using System.Collections.Generic;
using Membrane.Models.UserManagement;

namespace Membrane.Models.Interfaces.UserManagement
{
    public interface IUser : IModelBase
    {
        string Name { get; set; }
        UserType Type { get; set; }
        IList<Group> Groups { get; set; }
        string Username { get; set; }
        string Email { get; set; }
		string Password { get; set; }
    }
}