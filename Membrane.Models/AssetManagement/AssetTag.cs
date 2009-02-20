using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Membrane.Models.AssetManagement;
using Membrane.Models.Interfaces.AssetManagement;

namespace Membrane.Models.AssetManagement
{
    /// <summary>
    /// Tags can be stored against items and folders.  
    /// Remember to only store the tag name once
    /// </summary>
    [ActiveRecord]
    public class AssetTag : ModelBase, IAssetTag
    {
        private string tag;
        private IList<AssetFolder> folders;
        private IList<AssetItem> assets;

        [Property (NotNull = true, Length = 200), ValidateNonEmpty]
		public virtual string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        [HasAndBelongsToMany(typeof(AssetFolder), Table = "AssetFolderTags", ColumnKey = "AssetTagId", ColumnRef = "AssetFolderId", Cascade = ManyRelationCascadeEnum.None)]
		public virtual IList<AssetFolder> Folders
        {
            get { return folders; }
            set { folders = value; }
        }

        [HasAndBelongsToMany(typeof(AssetItem), Table = "AssetItemTags", ColumnKey = "AssetTagId", ColumnRef = "AssetItemId", Cascade = ManyRelationCascadeEnum.None)]
		public virtual IList<AssetItem> Assets
        {
            get { return assets; }
            set { assets = value; }
        }
    }
}