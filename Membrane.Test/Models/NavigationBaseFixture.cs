using Membrane.Models.NavigationManagement;
using NUnit.Framework;
using Rhino.Commons;

namespace Membrane.Test.Models
{
    public class NavigationBaseFixture : BaseFixture
    {
        protected static NavigationType CanCreateAndRetrieveNavigationType(string navTypeName)
        {
            NavigationType navType = CreateNavigationType(navTypeName);

            Assert.AreEqual(0, navType.Id);

            Repository<NavigationType>.Save(navType);
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.CurrentSession.Evict(navType);

            NavigationType fromDb = Repository<NavigationType>.Get(navType.Id);
            Assert.AreNotSame(fromDb, navType);

            Assert.AreEqual(navTypeName, fromDb.Name);
            Assert.AreEqual(false, fromDb.IsLocked);
            Assert.AreEqual(navType.Id, fromDb.Id);

            return fromDb;
        }

        protected static NavigationType CreateNavigationType(string navTypeName)
        {
            NavigationType navType = new NavigationType();
            navType.Name = navTypeName;
            navType.IsLocked = false;
            return navType;
        }
    }
}