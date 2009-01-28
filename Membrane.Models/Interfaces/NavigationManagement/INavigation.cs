using Membrane.Models.NavigationManagement;

namespace Membrane.Models.Interfaces.NavigationManagement
{
    public interface INavigation : IModelBase
    {
        string Name { get; set; }
        string Link { get; set; }
        int Order { get; set; }
        Navigation ParentNode { get; set; }
        NavigationType NavigationType { get; set; }
    }
}