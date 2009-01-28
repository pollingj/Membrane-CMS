using System.Collections.Generic;
using Membrane.Models.AssetManagement;

namespace Membrane.Models.Interfaces.AssetManagement
{
    public interface IAssetItem : IModelBase
    {
        string Url { get; set; }
        string Caption { get; set; }
        AssetFolder Folder { get; set; }
        string Source { get; set; }
        IList<AssetTag> Tags { get; set; }
        bool IsDeleted { get; set; }
    }
}