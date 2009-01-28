using System.Collections.Generic;
using Membrane.Models.AssetManagement;

namespace Membrane.Models.Interfaces.AssetManagement
{
    public interface IAssetFolder : IModelBase
    {
        string Name { get; set; }
        IList<AssetTag> Tags { get; set; }
        AssetFolder ParentFolder { get; set; }
        bool IsDeleted { get; set; }
    }
}