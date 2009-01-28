using Membrane.Models.NavigationManagement;
using NUnit.Framework;
using Rhino.Commons;

namespace Membrane.Test.Models
{
    [TestFixture]
    public class NavigationTypeFixture : NavigationBaseFixture
    {
        [Test]
        public void CanSaveNavigationType()
        {
            CanCreateAndRetrieveNavigationType("New Navigation Type");
        }


        [Test]
        public void CanEditNavigationType()
        {
            var fromDb = CanCreateAndRetrieveNavigationType("New Navigation Type");
            fromDb.Name = "Edited Nav Type";
            fromDb.IsLocked = true;
            Repository<NavigationType>.Update(fromDb);

            var edited = Repository<NavigationType>.Get(fromDb.Id);

            Assert.AreEqual("Edited Nav Type", edited.Name);
            Assert.AreEqual(true, edited.IsLocked);
            Assert.AreEqual(fromDb.Id, edited.Id);
        }

        [Test]
        public void CanDeleteNavigationType()
        {
            var fromDb = CanCreateAndRetrieveNavigationType("New Navigation Type");

            Repository<NavigationType>.Delete(fromDb);

            var deleted = Repository<NavigationType>.Get(fromDb.Id);

            Assert.IsNull(deleted);
        }
    }
}