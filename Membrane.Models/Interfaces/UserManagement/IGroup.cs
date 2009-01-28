using System.Collections.Generic;

namespace Membrane.Models.Interfaces.UserManagement
{
    public interface IGroup : IModelBase
    {
        string Name { get; set; }
        IList<Models.UserManagement.User> Users { get; set; }
    }
}