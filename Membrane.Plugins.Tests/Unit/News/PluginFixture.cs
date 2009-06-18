using AutoMapper;
using Membrane.Commons.Mappers;
using Membrane.Plugins.News;
using NUnit.Framework;

namespace Membrane.Plugins.Tests.Unit.News
{
	[TestFixture]
	public class PluginFixture
	{
		private Plugin newsPlugin;

		[SetUp]
		public void SetUp()
		{
			newsPlugin = new Plugin();
			CommonsAutoMapperConfiguration.Configure();
		}

		[Test]
		public void CanInitialisePlugin()
		{
			newsPlugin.Initialize();
			Mapper.AssertConfigurationIsValid();
		}
	}
}