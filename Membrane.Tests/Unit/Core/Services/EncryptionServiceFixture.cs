using Membrane.Core.Services;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Core.Services
{
	[TestFixture]
	public class EncryptionServiceFixture
	{
		private EncryptionService service;

		[SetUp]
		public void SetUp()
		{
			service = new EncryptionService();
		}

		[Test]
		public void CanEncryptValueSuccessfully()
		{
			var result = service.Encrypt("password");

			Assert.AreEqual("B1-09-F3-BB-BC-24-4E-B8-24-41-91-7E-D0-6D-61-8B-90-08-DD-09-B3-BE-FD-1B-5E-07-39-4C-70-6A-8B-B9-80-B1-D7-78-5E-59-76-EC-04-9B-46-DF-5F-13-26-AF-5A-2E-A6-D1-03-FD-07-C9-53-85-FF-AB-0C-AC-BC-86", result);
		}
	}
}