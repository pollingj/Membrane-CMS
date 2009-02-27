
using Membrane.Commons.Persistence;
using Membrane.Core.Services;
using Membrane.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Test.Services
{
	[TestFixture]
	public class ContentServiceFixture
	{
		//private IRepository navigationTypeRepository;
		private IRepository<ContentType> contentTypeRepository;
		private IRepository<ContentElement> contentElementRepository;
		private ContentService service;
		private MockRepository mockery;

		private const string modelName = "NavigationType";

		[SetUp]
		public void SetUp()
		{
			mockery = new MockRepository();

			service = new ContentService(contentTypeRepository, contentElementRepository);
		}

		[Test]
		public void CanReturnTheCurrentContent()
		{
			//Repository<T>.FindAll();
			var data = service.GetCurrentElements(modelName);

			Assert.AreEqual(2, data.Count);
		}
	}
}