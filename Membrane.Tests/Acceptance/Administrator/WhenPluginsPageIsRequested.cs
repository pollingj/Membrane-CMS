using NUnit.Framework;
using WatiN.Core;

namespace Membrane.Tests.Acceptance.Administrator
{
	[TestFixture, Category("Acceptance")]
	public class WhenPluginsPageIsRequested : AcceptanceTestBase
	{
		private IE browser;

		private string listPluginUrl;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			browser = new IE(BuildUrl("Login", "Index"));
			CompleteLoginForm(browser, "johnpolling", "password");

			listPluginUrl = BuildUrl("Administrator", "Plugins", "List");
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			browser.Dispose();
		}

		[Test]
		public void AdministratorCanViewAvailablePluginsList()
		{
			browser.Link("Plugins").Click();
			browser.WaitForComplete();

			Assert.IsTrue(browser.ContainsText("Primary Navigation"));
			CheckDataListLILength(browser, 3);
			Assert.AreEqual(listPluginUrl, browser.Url);
		}

	}
}