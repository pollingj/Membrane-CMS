using System.Collections.Generic;
using Membrane.Models.AssetManagement;
using NUnit.Framework;
using Query;
using Rhino.Commons;

namespace Membrane.Test.Models
{
    public class AssetBaseFixture : BaseFixture
    {
        protected AssetFolder CanCreateAndRetrieveAssetFolder(string folderName)
        {
            return CanCreateAndRetrieveAssetFolder(folderName, null);
        }

        protected AssetFolder CanCreateAndRetrieveAssetFolder(string folderName, AssetFolder parentFolder)
        {
            AssetFolder folder = CreateAssetFolder(folderName, parentFolder);

            Assert.AreEqual(0, folder.Id);

            Repository<AssetFolder>.Save(folder);
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.CurrentSession.Evict(folder);

            AssetFolder fromDb = Repository<AssetFolder>.Get(folder.Id);
            
            Assert.AreNotSame(fromDb, folder);

            Assert.AreEqual(folderName, fromDb.Name);
            Assert.AreEqual(false, fromDb.IsDeleted);
            Assert.AreEqual(parentFolder, fromDb.ParentFolder);
            Assert.AreEqual(3, fromDb.Tags.Count);
            Assert.AreEqual("tag1", fromDb.Tags[0].Tag);
            Assert.AreEqual("tag2", fromDb.Tags[1].Tag);
            Assert.AreEqual("tag3", fromDb.Tags[2].Tag);

            return fromDb;
        }

        protected IList<AssetTag> CreateTags(string[] tagNames)
        {
            IList<AssetTag> tags = new List<AssetTag>();
            
            foreach(string tag in tagNames)
            {
                tags.Add(CanCreateAndRetrieveAssetTag(tag));
            }

            return tags;
        }

        protected AssetTag CanCreateAndRetrieveAssetTag(string tagName)
        {
            AssetTag newTag = new AssetTag();
            AssetTag fromDb;
            newTag.Tag = tagName;

            fromDb = Repository<AssetTag>.FindOne(Where.AssetTag.Tag.Eq(tagName));

            if (fromDb == null)
            {
                Repository<AssetTag>.Save(newTag);
                UnitOfWork.Current.TransactionalFlush();
                UnitOfWork.CurrentSession.Evict(newTag);
                fromDb = Repository<AssetTag>.Get(newTag.Id);
            }

            Assert.AreNotSame(fromDb, newTag);

            Assert.AreEqual(tagName, fromDb.Tag);

            return fromDb;
        }

        #region Private Methods

        private AssetFolder CreateAssetFolder(string folderName, AssetFolder parent)
        {
            AssetFolder folder = new AssetFolder();
            folder.Name = folderName;
            folder.ParentFolder = parent;
            folder.Tags = CreateTags(new string[] {"tag1", "tag2", "tag3"});
            folder.IsDeleted = false;

            return folder;
        }



        #endregion
    }
}