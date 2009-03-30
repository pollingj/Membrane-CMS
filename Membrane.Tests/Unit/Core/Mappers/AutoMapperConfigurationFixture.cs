using AutoMapper;
using Membrane.Core.Mappers;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Core.Mappers
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