using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Membrane.Models.Interfaces.AssetManagement;

namespace Membrane.Models.AssetManagement
{
    /// <summary>
    /// Model to store information regarding Asset folders
    /// </summary>
    [ActiveRecord]
    public class AssetFolder : ModelBase, IAssetFolder
    {
        private string name;
        private IList<AssetTag> tags;
        private AssetFolder parentFolder;
        private bool isDeleted;

        [Property (NotNull = true, Length = 200), ValidateNonEmpty]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [HasAndBelongsToMany(typeof(AssetTag), Table="AssetFolderTags", ColumnKey="AssetFolderId", ColumnRef="AssetTagId")]
        public IList<AssetTag> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        [BelongsTo("ParentId")]
        public AssetFolder ParentFolder
        {
            get { return parentFolder; }
            set { parentFolder = value; }
        }

        [Property]
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
    }
}