using System.Collections.Generic;
using Membrane.Models.UserManagement;
using NUnit.Framework;
using Rhino.Commons;

namespace Membrane.Test.Models
{
    public class UserFixture : UserBaseFixture
    {
        [Test]
        public void CanAddAUser()
        {
            CanCreateAndRetrieveUser("John Polling");
        }

        [Test]
        public void CanEditUser()
        {
            IList<Group> groups = new List<Group>();
            groups.Add(CanCreateAndRetrieveGroup("Navigation Editors"));
            groups.Add(CanCreateAndRetrieveGroup("Content Editors"));


            User user = CanCreateAndRetrieveUser("John Polling");

            user.Email = "edit@test.com";
            user.Username = "tester";
            user.Name = "Testing Name";
            user.Type = CanCreateAndRetrieveUserType("editor");
            user.Groups = groups;

            Repository<User>.Update(user);

            User fromDb = Repository<User>.Get(user.Id);

            Assert.AreEqual("Testing Name", fromDb.Name);
            Assert.AreEqual("edit@test.com", fromDb.Email);
            Assert.AreEqual("tester", fromDb.Username);
            Assert.AreEqual("editor", fromDb.Type.Name);
            Assert.AreEqual(2, fromDb.Groups.Count);
            Assert.AreEqual("Navigation Editors", fromDb.Groups[0].Name);
            Assert.AreEqual("Content Editors", fromDb.Groups[1].Name);

        }

        [Test]
        public void CanDeleteUser()
        {
            User user = CanCreateAndRetrieveUser("John Poling");

            Repository<User>.Delete(user);

            User fromDb = Repository<User>.Get(user.Id);

            Assert.IsNull(fromDb);
        }
    }
}