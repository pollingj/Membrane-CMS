using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Core
{
	public class BaseFixture
	{
		public MockRepository mockery;

		[SetUp]
		public virtual void SetUp()
		{
			mockery = new MockRepository();
		}

		[TearDown]
		public virtual void TearDown()
		{

		}
	}
}