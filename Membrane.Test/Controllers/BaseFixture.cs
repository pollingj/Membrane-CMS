using System.Reflection;
using NUnit.Framework;
using Rhino.Commons;
using Rhino.Commons.ForTesting;

namespace Membrane.Test.Controllers
{
    [TestFixture]
    public class BaseFixture : DatabaseTestFixtureBase
    {
        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            IntializeNHibernateAndIoC(PersistenceFramework.ActiveRecord,
                                      "Windsor.boo", MappingInfo.From(Assembly.Load("Membrane.Models")));

            CurrentContext.CreateUnitOfWork();
            CurrentContext.SetupDatabase(UnitOfWork.CurrentSession);
            CurrentContext.DisposeUnitOfWork();
        }

        [SetUp]
        public virtual void SetUp()
        {
            CurrentContext.CreateUnitOfWork();
        }

        [TearDown]
        public virtual void TearDown()
        {
            CurrentContext.DisposeUnitOfWork();
        }
    }
}