using Membrane.Commons.Plugin.Services;
using Membrane.Commons.Wrappers.Interfaces;
using Membrane.Tests.Unit.Core;

namespace Membrane.Tests.Unit.Commons.Plugin.Services
{
	public class PluginServiceFixture : BaseFixture
	{
		private IDirectory directory;
		private IAssembly assembly;

		private PluginService service;

		public override void SetUp()
		{
			base.SetUp();

			directory = mockery.DynamicMock<IDirectory>();
			assembly = mockery.DynamicMock<IAssembly>();

			service = new PluginService(assembly, directory);
		}
	}
}