using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Membrane.Models.AssetManagement;
using Membrane.Models.Interfaces.AssetManagement;

namespace Membrane.Models.AssetManagement
{
    /// <summary>
    /// An AssetItem can be any file that is stored on the asset system.
    /// </summary>
    [ActiveRecord]
    public class AssetItem : ModelBase, IAssetItem
    {
        private string url;
        private string caption;
        private AssetFolder folder;
        private string source;
        private IList<AssetTag> tags;
        private bool isDeleted;

        /// <summary>
        /// The url from where this asset may have been sourced
        /// </summary>
        [Property (Length = 300)]
		public virtual string Url
        {
            get { return url; }
            set { url = value; }
        }

        /// <summary>
        /// Default caption for this asset (could be used as alt and title text)
        /// </summary>
        [Property (NotNull = true, Length = 300), ValidateNonEmpty]
		public virtual string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        /// <summary>
        /// What folder is this asset stored in?
        /// </summary>
        [BelongsTo("FolderId")]
		public virtual AssetFolder Folder
        {
            get { return folder; }
            set { folder = value; }
        }

        /// <summary>
        /// Where was this asset taken from?
        /// </summary>
        [Property (Length = 300)]
		public virtual string Source
        {
            get { return source; }
            set { source = value; }
        }

        /// <summary>
        /// What tags have been assigned to this asset?
        /// </summary>
        [HasAndBelongsToMany(typeof(AssetTag), Table = "AssetItemTags", ColumnKey = "AssetItemId", ColumnRef = "AssetTagId")]
		public virtual IList<AssetTag> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        /// <summary>
        /// Has this asset be put into the recycle bin?
        /// </summary>
        [Property]
		public virtual bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
    }
}