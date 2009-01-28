using Membrane.Models.UserManagement;
using NUnit.Framework;
using Rhino.Commons;


namespace Membrane.Test.Models
{
    public class GroupFixture : UserBaseFixture
    {
        [Test]
        public void CanCreateAndRetrieveAGroup()
        {
            CanCreateAndRetrieveGroup("Test");
        }

        [Test]
        public void CanEditAGroup()
        {
            Group group = CanCreateAndRetrieveGroup("Test");

            group.Name = "Edited Group";

            Repository<Group>.Update(group);

            Group fromDb = Repository<Group>.Get(group.Id);

            Assert.AreEqual("Edited Group", fromDb.Name);
        }

        [Test]
        public void CanDeleteAGroup()
        {
            Group group = CanCreateAndRetrieveGroup("Test");

            Repository<Group>.Delete(group);

            Group fromDb = Repository<Group>.Get(group.Id);

            Assert.IsNull(fromDb);
        }
    }
}