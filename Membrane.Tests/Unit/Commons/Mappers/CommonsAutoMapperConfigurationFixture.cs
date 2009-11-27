using AutoMapper;
using Membrane.Commons.Mappers;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Commons.Mappers
{
	[TestFixture]
	public class CommonsAutoMapperConfigurationFixture
	{
		[Test]
		public void CanSuccessFullyMap()
		{
			CommonsAutoMapperConfiguration.Configure();
			Mapper.AssertConfigurationIsValid();
		}
	}
}