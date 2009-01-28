using Membrane.Models.AssetManagement;
using NUnit.Framework;
using Rhino.Commons;

namespace Membrane.Test.Models
{
    public class AssetFolderFixture : AssetBaseFixture
    {
        [Test]
        public void CanAddAssetFolder()
        {
            CanCreateAndRetrieveAssetFolder("First Folder");
        }

        [Test]
        public void CanEditAssetFolder()
        {
            AssetFolder folder = CanCreateAndRetrieveAssetFolder("First Folder");

            folder.Name = "Edited Folder";
            folder.Tags = CreateTags(new string[] {"tag2", "tag3", "tag4", "tag5"});

            Repository<AssetFolder>.Update(folder);
            AssetFolder fromDb = Repository<AssetFolder>.Get(folder.Id);
            
            Assert.AreEqual("Edited Folder", fromDb.Name);
            Assert.AreEqual(4, fromDb.Tags.Count);
            Assert.AreEqual("tag2", fromDb.Tags[0].Tag);
            Assert.AreEqual("tag3", fromDb.Tags[1].Tag);
            Assert.AreEqual("tag4", fromDb.Tags[2].Tag);
            Assert.AreEqual("tag5", fromDb.Tags[3].Tag);
        }

        [Test]
        public void CanDeleteAssetFolder()
        {
            AssetFolder folder = CanCreateAndRetrieveAssetFolder("Deletion Folder");

            Repository<AssetFolder>.Delete(folder);
            UnitOfWork.Current.TransactionalFlush();

            AssetFolder fromDb = Repository<AssetFolder>.Get(folder.Id);

            Assert.IsNull(fromDb);
        }

        [Test]
        public void CanCreateSubFolders()
        {
            AssetFolder parentFolder = CanCreateAndRetrieveAssetFolder("Parent Folder");

            AssetFolder childFolder = CanCreateAndRetrieveAssetFolder("Child Folder", parentFolder);
            
            Assert.AreEqual("Child Folder", childFolder.Name);
            Assert.AreEqual(parentFolder, childFolder.ParentFolder);
        }
    }
}