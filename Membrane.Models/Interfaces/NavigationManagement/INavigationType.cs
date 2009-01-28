using System.Collections.Generic;
using Membrane.Models.NavigationManagement;

namespace Membrane.Models.Interfaces.NavigationManagement
{
    public interface INavigationType : IModelBase
    {
        string Name { get; set; }
        IList<Navigation> NavigationItems { get; set; }
        bool IsLocked { get; set; }
    }
}