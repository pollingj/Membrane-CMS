using System.Collections.Generic;
using Membrane.Models.UserManagement;
using NUnit.Framework;
using Rhino.Commons;

namespace Membrane.Test.Models
{
    public class UserTypeFixture : UserBaseFixture
    {
        [Test]
        public void CanAddAUserType()
        {
            CanCreateAndRetrieveUserType("Administrators");
        }

        [Test]
        public void CanEditUserType()
        {
            UserType userType = CanCreateAndRetrieveUserType("Administrators");

            userType.Name = "Editors";

            Repository<UserType>.Update(userType);

            UserType fromDb = Repository<UserType>.Get(userType.Id);

            Assert.AreEqual("Editors", fromDb.Name);

        }

        [Test]
        public void CanAddUsersToAUserType()
        {
            IList<User> users = new List<User>();
            users.Add(CanCreateAndRetrieveUser("John Polling"));
            users.Add(CanCreateAndRetrieveUser("Richard Polling"));

            UserType userType = CanCreateAndRetrieveUserType("Administrators");

            userType.Users = users;

            Repository<UserType>.Update(userType);

            UserType fromDb = Repository<UserType>.Get(userType.Id);

            Assert.AreEqual(2, fromDb.Users.Count);
            Assert.AreEqual("John Polling", fromDb.Users[0].Name);
            Assert.AreEqual("Richard Polling", fromDb.Users[1].Name);

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