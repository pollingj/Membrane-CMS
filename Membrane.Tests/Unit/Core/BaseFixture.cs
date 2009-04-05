using Membrane.Core.Mappers;
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

			AutoMapperConfiguration.Configure();
		}

		[TearDown]
		public virtual void TearDown()
		{

		}
	}
}