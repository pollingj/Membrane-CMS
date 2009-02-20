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
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        [HasAndBelongsToMany(typeof(AssetTag), Table="AssetFolderTags", ColumnKey="AssetFolderId", ColumnRef="AssetTagId")]
		public virtual IList<AssetTag> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        [BelongsTo("ParentId")]
		public virtual AssetFolder ParentFolder
        {
            get { return parentFolder; }
            set { parentFolder = value; }
        }

        [Property]
		public virtual bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
    }
}