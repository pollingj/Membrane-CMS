using AutoMapper;
using NUnit.Framework;

namespace Membrane.Core.Mappers
{
	[TestFixture]
	public class AutoMapperConfigurationFixture
	{
		[Test]
		public void CanSuccessFullyMap()
		{
			AutoMapperConfiguration.Configure();
			Mapper.AssertConfigurationIsValid();
		}
	}
}