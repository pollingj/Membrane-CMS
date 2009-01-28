using System.Collections.Generic;
using Membrane.Models.UserManagement;

namespace Membrane.Models.Interfaces.UserManagement
{
    public interface IUserType
    {
        string Name { get; set; }
        IList<User> Users { get; set; }
    }
}