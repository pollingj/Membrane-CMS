using System.Collections.Generic;
using Membrane.Models.UserManagement;
using NUnit.Framework;
using Rhino.Commons;

namespace Membrane.Test.Models
{
    public class UserBaseFixture : BaseFixture
    {
        protected Group CanCreateAndRetrieveGroup(string name)
        {
            Group group = new Group();
            group.Name = name;


            Assert.AreEqual(0, group.Id);

            Repository<Group>.Save(group);
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.CurrentSession.Evict(group);

            Group fromDb = Repository<Group>.Get(group.Id);
            Assert.AreNotSame(fromDb, group);

            Assert.AreEqual(name, fromDb.Name);

            return fromDb;
        }

        protected UserType CanCreateAndRetrieveUserType(string name)
        {
            UserType type = new UserType();

            type.Name = name;

            Assert.AreEqual(0, type.Id);

            Repository<UserType>.Save(type);
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.CurrentSession.Evict(type);

            UserType fromDb = Repository<UserType>.Get(type.Id);
            Assert.AreNotSame(fromDb, type);

            Assert.AreEqual(name, fromDb.Name);

            return fromDb;
        }

        protected User CanCreateAndRetrieveUser(string name)
        {
            IList<Group> groups= new List<Group>();
            groups.Add(CanCreateAndRetrieveGroup("Administrators"));
            User user = new User();

            user.Name = name;
            user.Email = "test@test.com";
            user.Type = CanCreateAndRetrieveUserType("Admin");
            user.Username = "pollingj";
            user.Groups = groups;
            user.Password = "password";

            Assert.AreEqual(0, user.Id);

            Repository<User>.Save(user);
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.CurrentSession.Evict(user);

            User fromDb = Repository<User>.Get(user.Id);
            Assert.AreNotSame(fromDb, user);

            Assert.AreEqual(name, fromDb.Name);
            Assert.AreEqual("test@test.com", fromDb.Email);

            return fromDb;
        }

    }
}