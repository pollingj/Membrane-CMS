using Membrane.Models.AssetManagement;
using NUnit.Framework;
using Rhino.Commons;

namespace Membrane.Test.Models
{
    public class AssetFixture : AssetBaseFixture
    {
        [Test]
        public void CanAddAsset()
        {
            CanCreateAndRetrieveAsset("First Item");
        }

        [Test]
        public void CanEditAsset()
        {
            AssetItem item = CanCreateAndRetrieveAsset("First Item");
            item.Url = "/uploads/test.gif";
            item.Caption = "This is the Edited Item";
            item.Source = "New Source";
            item.Tags = CreateTags(new string[] {"tag3", "tag4", "tag6"});

            Repository<AssetItem>.Update(item);

            AssetItem fromDb = Repository<AssetItem>.Get(item.Id);

            Assert.AreEqual("This is the Edited Item", fromDb.Caption);
            Assert.AreEqual(false, fromDb.IsDeleted);
            Assert.AreEqual(item.Folder, fromDb.Folder);
            Assert.AreEqual(3, fromDb.Tags.Count);
            Assert.AreEqual("tag3", fromDb.Tags[0].Tag);
            Assert.AreEqual("tag4", fromDb.Tags[1].Tag);
            Assert.AreEqual("tag6", fromDb.Tags[2].Tag);
            Assert.AreEqual("/uploads/test.gif", fromDb.Url);
            Assert.AreEqual("New Source", fromDb.Source);
        }

        [Test]
        public void CanDeleteAsset()
        {
            AssetItem item = CanCreateAndRetrieveAsset("To Be Deleted");

            Repository<AssetItem>.Delete(item);
            UnitOfWork.Current.TransactionalFlush();

            AssetItem deletedItem = Repository<AssetItem>.Get(item.Id);

            Assert.IsNull(deletedItem);
        }

        #region Private Methods

        public AssetItem CanCreateAndRetrieveAsset(string name)
        {
            AssetFolder folder = CanCreateAndRetrieveAssetFolder("Item Folder");

            AssetItem item = new AssetItem();
            item.Caption = name;
            item.Folder = folder;
            item.Url = "/uploads/item/asset.gif";
            item.Source = "Getty Images (125122)";
            item.Tags = CreateTags(new string[] { "tag1", "tag2" });

            Repository<AssetItem>.Save(item);
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.CurrentSession.Evict(item);

            AssetItem fromDb = Repository<AssetItem>.Get(item.Id);
            Assert.AreNotSame(fromDb, item);

            Assert.AreEqual(name, fromDb.Caption);
            Assert.AreEqual(false, fromDb.IsDeleted);
            Assert.AreEqual(folder, fromDb.Folder);
            Assert.AreEqual(2, fromDb.Tags.Count);
            Assert.AreEqual("tag1", fromDb.Tags[0].Tag);
            Assert.AreEqual("tag2", fromDb.Tags[1].Tag);
            Assert.AreEqual("/uploads/item/asset.gif", fromDb.Url);
            Assert.AreEqual("Getty Images (125122)", fromDb.Source);

            return fromDb;
        }

        #endregion
    }
}