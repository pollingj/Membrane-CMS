using Membrane.Models.NavigationManagement;
using NUnit.Framework;
using Rhino.Commons;

namespace Membrane.Test.Models
{
    public class NavigationFixture : NavigationBaseFixture
    {
        private NavigationType navType;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            navType = CanCreateAndRetrieveNavigationType("New Navigation Type");
        }

        [Test]
        public void CanAddNavigationItem()
        {
            CanCreateAndRetrieveNavigation("New Nav Item");
        }

        [Test] 
        public void CanAddSubNavigationItem()
        {
            var parentNav = CanCreateAndRetrieveNavigation("Parent Nav Item");

            Navigation navItem = CanCreateAndRetrieveNavigation("New Nav Item", parentNav);

            Assert.AreEqual("New Nav Item", navItem.Name);
            Assert.AreEqual("http://www.test.com", navItem.Link);
            Assert.AreEqual(1, navItem.Order);
            Assert.AreEqual(parentNav, navItem.ParentNode);
            Assert.AreEqual(navType, navItem.NavigationType);
        }

        [Test]
        public void CanEditNavigationItem()
        {
            Navigation navItem = CanCreateAndRetrieveNavigation("New Nav Item");
            NavigationType newNavigationType = CanCreateAndRetrieveNavigationType("Secondary Navigation Type");

            navItem.Name = "Edited Nav Item";
            navItem.Link = "http://www.theusualsuspect.com";
            navItem.Order = 2;
            navItem.NavigationType = newNavigationType;

            Repository<Navigation>.Update(navItem);

            Navigation edited = Repository<Navigation>.Get(navItem.Id);

            Assert.AreEqual("Edited Nav Item", edited.Name);
            Assert.AreEqual("http://www.theusualsuspect.com", edited.Link);
            Assert.AreEqual(2, edited.Order);
            Assert.AreEqual(newNavigationType, edited.NavigationType);
        }

        [Test]
        public void CanDeleteNavigationItem()
        {
            Navigation navItem = CanCreateAndRetrieveNavigation("New Nav Item");

            Repository<Navigation>.Delete(navItem);
            UnitOfWork.Current.TransactionalFlush();

            Navigation deletedItem = Repository<Navigation>.Get(navItem.Id);

            Assert.IsNull(deletedItem);
        }

        #region Private Methods

        private Navigation CanCreateAndRetrieveNavigation(string navName)
        {
            return CanCreateAndRetrieveNavigation(navName, null);
        }

        private Navigation CanCreateAndRetrieveNavigation(string navName, Navigation parentNode)
        {
            Navigation navItem = CreateNavigationItem(navName, parentNode);

            Assert.AreEqual(0, navItem.Id);

            Repository<Navigation>.Save(navItem);
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.CurrentSession.Evict(navItem);

            Navigation fromDb = Repository<Navigation>.Get(navItem.Id);
            Assert.AreNotSame(fromDb, navItem);

            Assert.AreEqual(navName, fromDb.Name);
            Assert.AreEqual("http://www.test.com", navItem.Link);
            Assert.AreEqual(1, navItem.Order);
            Assert.AreEqual(parentNode, navItem.ParentNode);
            Assert.AreEqual(navType, navItem.NavigationType);

            return fromDb;
        }


        private Navigation CreateNavigationItem(string navName, Navigation parentnode)
        {
            Navigation navItem = new Navigation
            {
                Name = navName,
                Link = "http://www.test.com",
                Order = 1,
                ParentNode = parentnode,
                NavigationType = navType
            };

            return navItem;
        }

        #endregion
    }
}