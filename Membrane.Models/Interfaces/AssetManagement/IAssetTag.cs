using System.Collections.Generic;
using Membrane.Models.AssetManagement;

namespace Membrane.Models.Interfaces.AssetManagement
{
    public interface IAssetTag : IModelBase
    {
        string Tag { get; set; }
        IList<AssetFolder> Folders { get; set; }
        IList<AssetItem> Assets { get; set; }
    }
}